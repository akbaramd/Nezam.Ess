using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Bonyan.Layer.Domain.ValueObjects;

namespace Nezam.Modular.ESS.Identity.Domain.Shared.User
{
    public class UserNameValue : BonValueObject
    {
        public string Value { get; private set; }
        protected UserNameValue() { }
        // Constructor to create a user name
        public UserNameValue(string userName)
        {
            if (string.IsNullOrWhiteSpace(userName) || userName.Length < 3 || userName.Length > 20)
                throw new ArgumentException("Username must be between 3 and 20 characters.");

            // Check if username matches a specific pattern (e.g., alphanumeric)
            if (!Regex.IsMatch(userName, @"^[a-zA-Z0-9]+$"))
                throw new ArgumentException("Username can only contain alphanumeric characters.");

            Value = userName;
        }

        protected override IEnumerable<object?> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}