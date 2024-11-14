using Bonyan.UserManagement.Domain;
using Bonyan.UserManagement.Domain.ValueObjects;
using Nezam.Modular.ESS.Identity.Domain.Employer;
using Nezam.Modular.ESS.Identity.Domain.Engineer;
using Nezam.Modular.ESS.Identity.Domain.Roles;
using Nezam.Modular.ESS.Identity.Domain.Shared.Employer;
using Nezam.Modular.ESS.Identity.Domain.Shared.Engineer;
using Nezam.Modular.ESS.Identity.Domain.Shared.Roles;
using Nezam.Modular.ESS.Identity.Domain.Shared.User.Events;

namespace Nezam.Modular.ESS.Identity.Domain.User
{
    public class UserEntity : BonUser
    {
        private readonly List<UserRoleEntity> _roleIds = new();
        private readonly List<UserVerificationTokenEntity> _verificationTokens = new();

        // Protected constructor for ORM
        protected UserEntity() { }

        private UserEntity(BonUserId bonUserId, string userName) 
            : base(bonUserId, userName) 
        {
            IsActive = true; // default to active
        }

        public static UserEntity Create(BonUserId bonUserId, string userName)
        {
            if (string.IsNullOrWhiteSpace(userName)) throw new ArgumentException("Username cannot be empty.", nameof(userName));
            return new UserEntity(bonUserId, userName);
        }

        public IReadOnlyCollection<UserRoleEntity> Roles  => _roleIds;
        public IReadOnlyCollection<UserVerificationTokenEntity> VerificationTokens => _verificationTokens.AsReadOnly();

        public EngineerEntity? Engineer { get; private set; }
        public EmployerEntity? Employer { get; private set; }
        public bool IsActive { get; private set; }

        // Role Management

        public void AssignRole(RoleId roleId)
        {
            if (_roleIds.Any(c=>c.RoleId == roleId))
                return;

            _roleIds.Add(new UserRoleEntity(Id,roleId));
            AddDomainEvent(new RoleAssignedEvent(Id, roleId));
        }

        public void RemoveRole(RoleId roleId)
        {
            if (_roleIds.Remove(_roleIds.First(x=>x.RoleId == roleId)))
            {
                AddDomainEvent(new RoleRemovedEvent(Id, roleId));
            }
        }

        // Verification Token Management

        public UserVerificationTokenEntity GenerateVerificationToken(UserVerificationTokenType type)
        {
            var token = new UserVerificationTokenEntity(type)
            {
                User = this,
                BonUserId = this.Id
            };

            _verificationTokens.Add(token);
            return token;
        }

        public bool RemoveVerificationToken(UserVerificationTokenEntity token)
        {
            if (token == null) throw new ArgumentNullException(nameof(token));
            return _verificationTokens.Remove(token);
        }

        public UserVerificationTokenEntity GetVerificationToken(UserVerificationTokenType type) =>
            _verificationTokens.FirstOrDefault(t => t.Type == type);

        // Contact Information Management

        public void UpdateContactInfo(string email, string phoneNumber)
        {
            if (string.IsNullOrWhiteSpace(phoneNumber)) throw new ArgumentException("Phone number cannot be empty.", nameof(phoneNumber));
            SetEmail(new Email(email));
            SetPhoneNumber(new PhoneNumber(phoneNumber));
            AddDomainEvent(new ContactInfoUpdatedEvent(this.Id, email, phoneNumber));
        }

        // Engineer and Employer Management

        public void AssignOrUpdateEngineer(string firstName, string? lastName, string membershipCode)
        {
            if (string.IsNullOrWhiteSpace(firstName)) throw new ArgumentException("First name cannot be empty.", nameof(firstName));
            if (string.IsNullOrWhiteSpace(membershipCode)) throw new ArgumentException("Membership code is required.", nameof(membershipCode));

            if (Engineer == null)
            {
                // Create a new EngineerEntity if one doesn't already exist
                Engineer = new EngineerEntity(EngineerId.CreateNew(), this.Id, firstName, lastName, membershipCode);
                AddDomainEvent(new EngineerAssignedOrUpdatedEvent(this.Id, Engineer.Id));
            }
            else
            {
                // Update existing EngineerEntity details
                Engineer.UpdateDetails(firstName, lastName, membershipCode);
                AddDomainEvent(new EngineerAssignedOrUpdatedEvent(this.Id, Engineer.Id));
            }
        }
        public void AssignOrUpdateEmployer(string firstName, string? lastName)
        {
            if (string.IsNullOrWhiteSpace(firstName)) throw new ArgumentException("First name cannot be empty.", nameof(firstName));

            if (Employer == null)
            {
                Employer = new EmployerEntity(EmployerId.CreateNew(), Id, firstName, lastName);
                AddDomainEvent(new EmployerAssignedOrUpdatedEvent(this.Id, Employer.Id));
            }
            else
            {
                Employer.UpdateDetails(firstName, lastName);
                AddDomainEvent(new EmployerAssignedOrUpdatedEvent(this.Id, Employer.Id));
            }
        }

        public void RemoveEngineer()
        {
            if (Engineer != null)
            {
                var engineerId = Engineer.Id;
                Engineer = null;
                AddDomainEvent(new EngineerRemovedEvent(this.Id, engineerId));
            }
        }

        public void RemoveEmployer()
        {
            if (Employer != null)
            {
                var employerId = Employer.Id;
                Employer = null;
                AddDomainEvent(new EmployerRemovedEvent(this.Id, employerId));
            }
        }

        // Activation Management

        public void Activate()
        {
            if (!IsActive)
            {
                IsActive = true;
                AddDomainEvent(new UserActivatedEvent(this.Id));
            }
        }

        public void Deactivate()
        {
            if (IsActive)
            {
                IsActive = false;
                AddDomainEvent(new UserDeactivatedEvent(this.Id));
            }
        }

    }
}
