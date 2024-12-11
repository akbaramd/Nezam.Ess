using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Bonyan.Layer.Domain.ValueObjects;

namespace Nezam.Modular.ESS.Identity.Domain.Shared.User
{
    public class UserEmailValue : BonValueObject
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
        private bool IsValidEmail(string email)
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