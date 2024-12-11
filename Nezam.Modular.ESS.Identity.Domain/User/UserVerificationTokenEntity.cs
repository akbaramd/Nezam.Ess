using Bonyan.Layer.Domain.Entities;
using Bonyan.Layer.Domain.Enumerations;
using Bonyan.Layer.Domain.ValueObjects;
using Bonyan.UserManagement.Domain.Users.ValueObjects;
using Nezam.Modular.ESS.Identity.Domain.Shared.User;

namespace Nezam.Modular.ESS.Identity.Domain.User;

public class UserVerificationTokenEntityKey : BonBusinessId<UserVerificationTokenEntityKey>
{
    
}
public class UserVerificationTokenEntity : BonEntity<UserVerificationTokenEntityKey>
{
    public UserVerificationTokenType Type { get;   set; }
    public string Token { get;  set; }

    public UserEntity User { get; set; }
    public UserId UserId { get; set; }

    protected UserVerificationTokenEntity()
    {
    }

    public UserVerificationTokenEntity(UserVerificationTokenType type)
    {
        Id = UserVerificationTokenEntityKey.NewId();
        Type = type;
        Token = Guid.NewGuid().ToString();
    }

   
}

public class UserVerificationTokenType : BonEnumeration
{
    public static readonly UserVerificationTokenType Global = new UserVerificationTokenType(0, nameof(Global));
    public static readonly UserVerificationTokenType ForgetPassword = new UserVerificationTokenType(1, nameof(ForgetPassword));

    public UserVerificationTokenType():base(0,nameof(Global)){}
    public UserVerificationTokenType(int id, string name)
        : base(id, name)
    {
    }
}