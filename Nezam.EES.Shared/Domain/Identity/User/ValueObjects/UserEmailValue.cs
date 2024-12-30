using System.Text.RegularExpressions;
using Payeh.SharedKernel.Domain.ValueObjects;

namespace Nezam.EEs.Shared.Domain.Identity.User.ValueObjects
{
    public class UserEmailValue : ValueObject
    {
        public string Value { get; private set; }

        protected UserEmailValue() { }
        // Constructor to create an email value object
        public UserEmailValue(string email)
        {
            if (string.IsNullOrWhiteSpace(email) || !IsValidEmail(email))
                throw new ArgumentException("Invalid email format.");

            Value = email;
        }

        // Check if the email format is valid
        public static bool IsValidEmail(string email)
        {
            var emailRegex = new Regex(@"^[^@]+@[^@]+\.[^@]+$");
            return emailRegex.IsMatch(email);
        }

        protected override IEnumerable<object?> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}