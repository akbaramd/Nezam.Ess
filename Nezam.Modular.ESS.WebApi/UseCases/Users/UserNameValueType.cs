using Nezam.Modular.ESS.Identity.Domain.Shared.User;

public class UserNameValueScalarType : ValueObjectScalarType<UserNameValue>
{
    public UserNameValueScalarType() : base("UserNameValues") { }

    protected override UserNameValue CreateFromValue(string value)
    {
        return new UserNameValue(value);
    }

    protected override string GetValue(UserNameValue runtimeValue)
    {
        return runtimeValue.Value;
    }
}

public class UserIdScalarType : ValueObjectScalarType<UserId>
{
    public UserIdScalarType() : base("UserIds") { }

    protected override UserId CreateFromValue(string value)
    {
        return  UserId.NewId(Guid.Parse(value));
    }

    protected override string GetValue(UserId runtimeValue)
    {
        return runtimeValue.Value.ToString();
    }
}
