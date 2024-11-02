using Bonyan.Layer.Domain.Entities;
using Bonyan.Layer.Domain.Enumerations;
using Bonyan.UserManagement.Domain.ValueObjects;

namespace Nezam.Modular.ESS.Identity.Domain.User;

public class UserVerificationTokenEntity : Entity
{
    public UserVerificationTokenType Type { get; private  set; }
    public string Token { get; private set; }

    public UserEntity User { get; set; }
    public UserId UserId { get; set; }

    protected UserVerificationTokenEntity()
    {
    }

    public UserVerificationTokenEntity(UserVerificationTokenType type)
    {
        Type = type;
        Token = Guid.NewGuid().ToString();
    }

    public override object[] GetKeys()
    {
        return [Token];
    }
}

public class UserVerificationTokenType : Enumeration
{
    public static UserVerificationTokenType Global = new UserVerificationTokenType(0, nameof(Global));
    public static UserVerificationTokenType ForgetPassword = new UserVerificationTokenType(1, nameof(ForgetPassword));
    public UserVerificationTokenType(int id, string name) : base(id, name)
    {
    }
}