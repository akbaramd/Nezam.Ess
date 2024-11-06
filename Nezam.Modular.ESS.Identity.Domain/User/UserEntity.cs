using System;
using System.Collections.Generic;
using System.Linq;
using Bonyan.Layer.Domain.ValueObjects;
using Bonyan.UserManagement.Domain;
using Bonyan.UserManagement.Domain.ValueObjects;
using Nezam.Modular.ESS.Identity.Domain.Employer;
using Nezam.Modular.ESS.Identity.Domain.Engineer;
using Nezam.Modular.ESS.Identity.Domain.Roles;

namespace Nezam.Modular.ESS.Identity.Domain.User
{
    public class UserEntity : BonyanUser
    {
        private readonly List<RoleEntity> _roles = new List<RoleEntity>();
        private readonly List<UserVerificationTokenEntity> _verificationTokens = new List<UserVerificationTokenEntity>();

        protected UserEntity()
        {
        }

        public UserEntity(UserId userId, string userName) : base(userId, userName)
        {
        }

        public IReadOnlyCollection<RoleEntity> Roles => _roles;
        public IReadOnlyCollection<UserVerificationTokenEntity> VerificationTokens => _verificationTokens;

        public EngineerEntity Engineer { get; private set; }
        public EmployerEntity Employer { get; private set; }

        // Role Management

        /// <summary>
        /// Assigns a role to the user if it doesn't already exist.
        /// </summary>
        public void TryAssignRole(RoleEntity role)
        {
            if (role == null) throw new ArgumentNullException(nameof(role));

            if (_roles.All(r => r.Name != role.Name))
            {
                _roles.Add(role);
            }
        }

        /// <summary>
        /// Removes a role from the user if it exists.
        /// </summary>
        public void TryRemoveRole(RoleEntity role)
        {
            if (role == null) throw new ArgumentNullException(nameof(role));

            var existingRole = _roles.FirstOrDefault(r => r.Name == role.Name);
            if (existingRole != null)
            {
                _roles.Remove(existingRole);
            }
        }

        // Verification Token Management

        /// <summary>
        /// Generates a new verification token for the user.
        /// </summary>
        public UserVerificationTokenEntity GenerateVerificationToken(UserVerificationTokenType type)
        {
            var token = new UserVerificationTokenEntity(type)
            {
                User = this,
                UserId = this.Id
            };
            _verificationTokens.Add(token);
            return token;
        }

        /// <summary>
        /// Removes a specific verification token from the user.
        /// </summary>
        public bool RemoveVerificationToken(UserVerificationTokenEntity token)
        {
            if (token == null) throw new ArgumentNullException(nameof(token));

            return _verificationTokens.Remove(token);
        }

        /// <summary>
        /// Retrieves a specific type of active verification token if it exists.
        /// </summary>
        public UserVerificationTokenEntity GetVerificationToken(UserVerificationTokenType type)
        {
            return _verificationTokens.FirstOrDefault(t => t.Type == type);
        }

        // Contact Information Management

        /// <summary>
        /// Updates the user's email and phone number.
        /// </summary>
        public void UpdateContactInfo(string email, string phoneNumber)
        {
            if (string.IsNullOrWhiteSpace(email)) throw new ArgumentException("Email cannot be empty.", nameof(email));
            if (string.IsNullOrWhiteSpace(phoneNumber)) throw new ArgumentException("Phone number cannot be empty.", nameof(phoneNumber));

            SetEmail(new Email(email));
            SetPhoneNumber(new PhoneNumber(phoneNumber));
        }

        // Engineer and Employer Management

        /// <summary>
        /// Assigns an Engineer profile to the user.
        /// </summary>
        public void AssignEngineer(EngineerEntity engineer)
        {
            if (engineer == null) throw new ArgumentNullException(nameof(engineer));

            Engineer = engineer;
        }

        /// <summary>
        /// Assigns an Employer profile to the user.
        /// </summary>
        public void AssignEmployer(EmployerEntity employer)
        {
            if (employer == null) throw new ArgumentNullException(nameof(employer));

            Employer = employer;
        }

        /// <summary>
        /// Removes the Engineer profile from the user.
        /// </summary>
        public void RemoveEngineer()
        {
            Engineer = null;
        }

        /// <summary>
        /// Removes the Employer profile from the user.
        /// </summary>
        public void RemoveEmployer()
        {
            Employer = null;
        }
    }
}
