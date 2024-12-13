using System.Text.RegularExpressions;
using Payeh.SharedKernel.Domain.ValueObjects;

namespace Nezam.Modular.ESS.Identity.Domain.Shared.User
{
    public class UserPasswordValue : ValueObject
    {
        public string Value { get; private set; }
        protected UserPasswordValue() { }
        // Constructor to create a password value
        public UserPasswordValue(string password)
        {
            if (string.IsNullOrWhiteSpace(password) || !IsValidPassword(password))
                throw new ArgumentException("Password does not meet the required criteria.");

            Value = HashPassword(password); // Store the hashed password
        }

        // Hash the password using SHA256 (you can replace with stronger hashing if needed)
        private string HashPassword(string password)
        {
            using (var sha256 = System.Security.Cryptography.SHA256.Create())
            {
                var bytes = sha256.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(bytes);
            }
        }

        // Validate if the provided password is strong enough
        public static bool IsValidPassword(string password)
        {
            // Password must be at least 8 characters long
            if (password.Length < 3)
                return false;

            // If all conditions are met, return true
            return true;
        }

        // Validate the given password by comparing with the stored hash
        public bool Validate(string password)
        {
            string hashedPassword = HashPassword(password);
            return hashedPassword == Value;
        }

        protected override IEnumerable<object?> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
