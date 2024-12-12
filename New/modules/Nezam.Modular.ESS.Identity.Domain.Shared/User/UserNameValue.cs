using System.Text.RegularExpressions;
using Payeh.SharedKernel.Domain.ValueObjects;

namespace Nezam.Modular.ESS.Identity.Domain.Shared.User;

public class UserNameValue : ValueObject
{
    private string _value;

    public string Value
    {
        get => _value;
        private set => _value = value;
    }

    public UserNameValue( string value)
    {
        if (string.IsNullOrWhiteSpace(value) || value.Length < 3 || value.Length > 20)
            throw new ArgumentException("Username must be between 3 and 20 characters.");

        if (!Regex.IsMatch(value, @"^[a-zA-Z0-9_-]+$"))
            throw new ArgumentException("Username can only contain alphanumeric characters, underscores, and hyphens.");

        Value = value;
    }

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        return [Value];
    }
}