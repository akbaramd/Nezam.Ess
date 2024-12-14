using Payeh.SharedKernel.Domain.ValueObjects;

namespace Nezam.EEs.Shared.Domain.Identity.User.ValueObjects
{
    public class UserProfileValue : ValueObject
    {
        protected UserProfileValue() { }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }

        // Constructor to create a user profile value object
        public UserProfileValue( string firstName, string lastName)
        {
            if (string.IsNullOrWhiteSpace(firstName))
                throw new ArgumentException("First name cannot be null or empty.");
            
            if (string.IsNullOrWhiteSpace(lastName))
                throw new ArgumentException("Last name cannot be null or empty.");


            FirstName = firstName;
            LastName = lastName;
        }

        protected override IEnumerable<object?> GetEqualityComponents()
        {
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

            FirstName = newFirstName;
            LastName = newLastName;
        }
    }
}
