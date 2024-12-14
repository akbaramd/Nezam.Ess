using Nezam.Modular.ESS.Identity.Domain.Roles;
using Payeh.SharedKernel.Domain;

namespace Nezam.Modular.ESS.Identity.Domain.User
{
    public class UserEntity : AggregateRoot
    {
        public UserId UserId { get; set; }
        public UserNameValue UserName { get; private set; }
        public UserPasswordValue Password { get; private set; }
        public UserEmailValue? Email { get; private set; }
        public UserProfileValue? Profile { get; private set; }
        public ICollection<UserVerificationTokenEntity> VerificationTokens { get; private set; }
        
        // Collection to hold assigned roles
        public ICollection<RoleEntity> Roles { get; private set; }
        protected UserEntity() { }

        // Constructor for creating a new user with profile and tokens
        public UserEntity(UserId userId, UserNameValue userName, UserPasswordValue password, UserProfileValue? profile=null, UserEmailValue? email = null)
        {
            UserId = userId;
            SetUserName(userName);
            SetPassword(password);
            UpdateProfile(profile);
            Email = email;
            VerificationTokens = new List<UserVerificationTokenEntity>(); // Initialize the token collection
            Roles = new List<RoleEntity>(); // Initialize the roles collection
            
            AddDomainEvent(new UserCreatedEvent(userId, userName.Value, email?.Value, profile));
        }

        // Method for changing the username
        public void SetUserName(UserNameValue newUserName)
        {
            if (newUserName == null || string.IsNullOrWhiteSpace(newUserName.Value))
                throw new ArgumentException("Username cannot be empty or null.");

            UserName = newUserName;
            AddDomainEvent(new UserNameUpdatedEvent(UserId, newUserName.Value));
        }

        public void SetPassword(UserPasswordValue newPassword)
        {
            if (newPassword == null)
                throw new ArgumentNullException(nameof(newPassword));

            Password = newPassword;
            AddDomainEvent(new UserPasswordUpdatedEvent(UserId));
        }

        public void SetEmail(UserEmailValue? newEmail)
        {
            Email = newEmail;
            AddDomainEvent(new UserEmailUpdatedEvent(UserId, newEmail?.Value));
        }

        public void UpdateProfile(UserProfileValue? newProfile)
        {
            Profile = newProfile;
            AddDomainEvent(new UserProfileUpdatedEvent(UserId, newProfile));
        }

        public void AddRole(RoleEntity role)
        {
            if (role == null)
                throw new ArgumentException("Role cannot be null.");

            if (!Roles.Contains(role))
            {
                Roles.Add(role);
                AddDomainEvent(new UserRoleAddedEvent(UserId, role.RoleId, role.Title));
            }
        }

        public void RemoveRole(RoleEntity role)
        {
            if (role == null)
                throw new ArgumentException("Role cannot be null.");

            if (Roles.Contains(role))
            {
                Roles.Remove(role);
                AddDomainEvent(new UserRoleRemovedEvent(UserId, role.RoleId, role.Title));
            }
        }



        // Method to check if the user has a specific role
        public bool HasRole(RoleEntity role)
        {
            return Roles.Contains(role); // Check if the user already has the specified role
        }

        // Validate if the user is allowed to change password
        public bool ValidatePassword(string password)
        {
            return Password.Validate(password);
        }

        public override object GetKey()
        {
            return new { UserId = UserId };
        }
    }
}
