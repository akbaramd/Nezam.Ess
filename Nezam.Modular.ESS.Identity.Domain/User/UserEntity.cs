using System;
using System.Collections.Generic;
using Nezam.Modular.ESS.Identity.Domain.Shared.Roles;
using Bonyan.Layer.Domain.Entities;
using Nezam.Modular.ESS.Identity.Domain.Roles;
using Nezam.Modular.ESS.Identity.Domain.Shared.User;

namespace Nezam.Modular.ESS.Identity.Domain.User
{
    public class UserEntity : BonEntity
    {
        public UserId UserId { get; set; }
        public UserNameValue UserName { get; private set; }
        public UserPasswordValue Password { get; private set; }
        public UserEmailValue? Email { get; private set; }
        public UserProfileValue Profile { get; private set; }
        public ICollection<UserVerificationTokenEntity> VerificationTokens { get; private set; }
        
        // Collection to hold assigned roles
        public ICollection<RoleEntity> Roles { get; private set; }

        protected UserEntity() { }

        // Constructor for creating a new user with profile and tokens
        public UserEntity(UserId userId, UserNameValue userName, UserPasswordValue password, UserProfileValue profile, UserEmailValue? email = null)
        {
            UserId = userId;
            SetUserName(userName);
            SetPassword(password);
            SetProfile(profile);
            Email = email;
            VerificationTokens = new List<UserVerificationTokenEntity>(); // Initialize the token collection
            Roles = new List<RoleEntity>(); // Initialize the roles collection
        }

        // Method for changing the username
        public void SetUserName(UserNameValue newUserName)
        {
            if (newUserName == null || string.IsNullOrWhiteSpace(newUserName.Value))
                throw new ArgumentException("Username cannot be empty or null.");

            UserName = newUserName;
        }

        // Method for changing the password
        public void SetPassword(UserPasswordValue newPassword)
        {
            Password = newPassword;
        }

        // Method for updating email
        public void SetEmail(UserEmailValue? newEmail)
        {
            Email = newEmail;
        }

        // Method for updating profile
        public void SetProfile(UserProfileValue newProfile)
        {
            if (newProfile == null)
                throw new ArgumentException("Profile cannot be null.");

            Profile = newProfile;
        }

        // Method to add a new role to the user
        public void AddRole(RoleEntity role)
        {
            if (role == null)
                throw new ArgumentException("Role cannot be null.");
            
            if (!Roles.Contains(role))
            {
                Roles.Add(role); // Add the role to the user's collection
            }
        }

        // Method to remove a role from the user
        public void RemoveRole(RoleEntity role)
        {
            if (role == null)
                throw new ArgumentException("Role cannot be null.");

            if (Roles.Contains(role))
            {
                Roles.Remove(role); // Remove the role from the user's collection
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
            return new { UserId };
        }
    }
}
