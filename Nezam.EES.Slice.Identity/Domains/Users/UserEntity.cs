using Nezam.EES.Service.Identity.Domains.Departments;
using Nezam.EES.Service.Identity.Domains.Roles;
using Nezam.EEs.Shared.Domain.Identity.User;
using Nezam.EEs.Shared.Domain.Identity.User.DomainEvents;
using Nezam.EEs.Shared.Domain.Identity.User.ValueObjects;
using Payeh.SharedKernel.Domain;

namespace Nezam.EES.Service.Identity.Domains.Users
{
    // Our main Aggregate Root representing a User in the domain
    public class UserEntity : AggregateRoot
    {
        // Strongly-typed user ID
        public UserId UserId { get; private set; }

        // Strongly-typed username
        public UserNameId UserName { get; private set; }

        // Password value object (stores hashed password, etc.)
        public UserPasswordValue Password { get; private set; }

        // Optional email for the user
        public UserEmailValue? Email { get; private set; }

        // Optional profile info
        public UserProfileValue? Profile { get; private set; }

        // Tokens associated with this user (JWT refresh tokens, etc.)
        public ICollection<UserTokenEntity> Tokens { get; private set; } = new List<UserTokenEntity>();

        // For controlling deletion logic
        public bool IsCanDelete { get; private set; } = true;
        public bool IsSoftDeleted { get; private set; } = false;

        // Roles assigned to the user (many-to-many)
        public ICollection<RoleEntity> Roles { get; private set; } = new List<RoleEntity>();

        // Departments this user belongs to (many-to-many)
        public ICollection<DepartmentEntity> Departments { get; private set; } = new List<DepartmentEntity>();

        // Protected constructor for EF
        protected UserEntity() { }

        // Main constructor for creating a new user
        public UserEntity(
            UserId userId, 
            UserNameId userName, 
            UserPasswordValue password, 
            UserProfileValue? profile = null, 
            UserEmailValue? email = null)
        {
            UserId = userId ?? throw new ArgumentNullException(nameof(userId));
            SetUserName(userName);       // Reuse domain behavior for validations
            SetPassword(password);
            UpdateProfile(profile);
            Email = email;

            // Default states
            IsCanDelete = true;
            IsSoftDeleted = false;

            // Initialize collections
            Tokens = new List<UserTokenEntity>();
            Roles = new List<RoleEntity>();
            Departments = new List<DepartmentEntity>();

            // Raise event
            AddDomainEvent(new UserCreatedEvent(userId, userName.Value, email?.Value, profile));
        }

        // Change username
        public void SetUserName(UserNameId newUserName)
        {
            if (newUserName == null || string.IsNullOrWhiteSpace(newUserName.Value))
                throw new ArgumentException("Username cannot be empty or null.", nameof(newUserName));

            UserName = newUserName;
            AddDomainEvent(new UserNameUpdatedEvent(UserId, newUserName.Value));
        }

        // Change password
        public void SetPassword(UserPasswordValue newPassword)
        {
            if (newPassword == null)
                throw new ArgumentNullException(nameof(newPassword));

            Password = newPassword;
            AddDomainEvent(new UserPasswordUpdatedEvent(UserId));
        }

        // Change email
        public void SetEmail(UserEmailValue? newEmail)
        {
            Email = newEmail;
            AddDomainEvent(new UserEmailUpdatedEvent(UserId, newEmail?.Value));
        }

        // Update profile
        public void UpdateProfile(UserProfileValue? newProfile)
        {
            Profile = newProfile;
            AddDomainEvent(new UserProfileUpdatedEvent(UserId, newProfile));
        }

        // Add a role to the user
        public void AddRole(RoleEntity role)
        {
            if (role == null)
                throw new ArgumentException("Role cannot be null.");

            if (!Roles.Contains(role))
            {
                Roles.Add(role);
                AddDomainEvent(new UserRoleUpdatedEvent(
                    UserId, Roles.Select(x => x.RoleId.Value.ToString()).ToArray()));
            }
        }

        // Remove a role
        public void RemoveRole(RoleEntity role)
        {
            if (role == null)
                throw new ArgumentException("Role cannot be null.");

            if (Roles.Contains(role))
            {
                Roles.Remove(role);
                AddDomainEvent(new UserRoleUpdatedEvent(
                    UserId, Roles.Select(x => x.RoleId.Value.ToString()).ToArray()));
            }
        }

        // Check for a role
        public bool HasRole(RoleEntity role)
        {
            return Roles.Contains(role);
        }

        // Add a department membership
        public void AddDepartment(DepartmentEntity department)
        {
            if (department == null)
                throw new ArgumentNullException(nameof(department));

            if (!Departments.Contains(department))
            {
                Departments.Add(department);
                AddDomainEvent(new UserDepartmentUpdatedEvent(
                    UserId, Departments.Select(x => x.DepartmentId).ToArray()));
                // Could raise a DepartmentUpdatedEvent if needed
            }
        }

        // Remove a department membership
        public void RemoveDepartment(DepartmentEntity department)
        {
            if (department == null)
                throw new ArgumentNullException(nameof(department));

            if (Departments.Contains(department))
            {
                Departments.Remove(department);
                // Could raise a DepartmentUpdatedEvent if needed
                AddDomainEvent(new UserDepartmentUpdatedEvent(
                    UserId, Departments.Select(x => x.DepartmentId).ToArray()));
            }
        }

        // Validate user password (comparing raw input to the stored hashed password)
        public bool ValidatePassword(string password)
        {
            return Password.Validate(password);
        }

        // Soft delete the user
        public void SoftDelete()
        {
            IsSoftDeleted = true;
        }

        // Mark the user as can delete
        public void CanDelete()
        {
            IsCanDelete = true;
        }

        // Mark the user as cannot delete
        public void CanNotDelete()
        {
            IsCanDelete = false;
        }

        // Return a composite key if needed
        public override object GetKey()
        {
            return new { UserId = UserId.Value };
        }
    }
}
