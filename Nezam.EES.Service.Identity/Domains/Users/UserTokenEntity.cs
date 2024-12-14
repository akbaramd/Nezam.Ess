using Nezam.EEs.Shared.Domain.Identity.User;
using Payeh.SharedKernel.Domain;
using Payeh.SharedKernel.Domain.Enumerations;
using Payeh.SharedKernel.Domain.ValueObjects;

namespace Nezam.EES.Service.Identity.Domains.Users;

public class UserTokenEntityId : GuidBusinessId<UserTokenEntityId>
{
    
}
public class UserTokenEntity : Entity
{
    public UserTokenEntityId TokenId { get; set; }
    public UserVerificationTokenType Type { get;   set; }
    public string Token { get;  set; }

    public UserEntity User { get; set; }
    public UserId UserId { get; set; }

    protected UserTokenEntity()
    {
    }

    public UserTokenEntity(UserVerificationTokenType type)
    {
        TokenId = UserTokenEntityId.NewId();
        Type = type;
        Token = Guid.NewGuid().ToString();
    }


    public override object GetKey()
    {
        return TokenId;
    }
}

public class UserVerificationTokenType : Enumeration
{
    public static readonly UserVerificationTokenType Global = new UserVerificationTokenType(0, nameof(Global));
    public static readonly UserVerificationTokenType ForgetPassword = new UserVerificationTokenType(1, nameof(ForgetPassword));

    public UserVerificationTokenType():base(0,nameof(Global)){}
    public UserVerificationTokenType(int id, string name)
        : base(id, name)
    {
    }
}