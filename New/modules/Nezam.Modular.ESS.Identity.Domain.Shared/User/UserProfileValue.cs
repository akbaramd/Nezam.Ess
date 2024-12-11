using System;
using System.Collections.Generic;
using Bonyan.Layer.Domain.ValueObjects;

namespace Nezam.Modular.ESS.Identity.Domain.Shared.User
{
    public class UserProfileValue : BonValueObject
    {
        protected UserProfileValue() { }
        public string Avatar { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }

        // Constructor to create a user profile value object
        public UserProfileValue(string avatar, string firstName, string lastName)
        {
            if (string.IsNullOrWhiteSpace(firstName))
                throw new ArgumentException("First name cannot be null or empty.");
            
            if (string.IsNullOrWhiteSpace(lastName))
                throw new ArgumentException("Last name cannot be null or empty.");

            // Optionally, validate Avatar (it can be a URL or path to an image)
            if (string.IsNullOrWhiteSpace(avatar))
                throw new ArgumentException("Avatar cannot be null or empty.");

            Avatar = avatar;
            FirstName = firstName;
            LastName = lastName;
        }

        protected override IEnumerable<object?> GetEqualityComponents()
        {
            yield return Avatar;
            yield return FirstName;
            yield return LastName;
        }

        // You can add any other methods related to profile updates here if needed, for example:
        public void UpdateProfile(string newAvatar, string newFirstName, string newLastName)
        {
            if (string.IsNullOrWhiteSpace(newFirstName))
                throw new ArgumentException("First name cannot be null or empty.");
            if (string.IsNullOrWhiteSpace(newLastName))
                throw new ArgumentException("Last name cannot be null or empty.");
            if (string.IsNullOrWhiteSpace(newAvatar))
                throw new ArgumentException("Avatar cannot be null or empty.");

            Avatar = newAvatar;
            FirstName = newFirstName;
            LastName = newLastName;
        }
    }
}
