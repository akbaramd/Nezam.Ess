using Bonyan.Layer.Domain.Aggregates;
using Bonyan.Layer.Domain.ValueObjects;
using Bonyan.UserManagement.Domain;
using Bonyan.UserManagement.Domain.ValueObjects;
using Nezam.Modular.ESS.Identity.Domain.Employer;
using Nezam.Modular.ESS.Identity.Domain.Engineer;
using Nezam.Modular.ESS.Identity.Domain.Roles;

namespace Nezam.Modular.ESS.Identity.Domain.User;

public class UserEntity : BonyanUser
{
    protected UserEntity()
    {
    }

    public UserEntity(UserId userId, string userName) : base(userId, userName)
    {
    }

    private List<RoleEntity> _roles = new List<RoleEntity>();
    public IReadOnlyCollection<RoleEntity> Roles => _roles;

    private List<UserVerificationTokenEntity> _verificationToken = new List<UserVerificationTokenEntity>();
    public IReadOnlyCollection<UserVerificationTokenEntity> VerificationTokens => _verificationToken;

    public EngineerEntity Engineer { get; set; }
    public EmployerEntity Employer { get; set; }

    public void TryAssignRole(RoleEntity role)
    {
        if (!Roles.Any(x => x.Name.Equals(role.Name)))
        {
            _roles.Add(role);
        }
    }

    public void TryRemoveRole(RoleEntity role)
    {
        var existingRole = _roles.FirstOrDefault(x => x.Name.Equals(role.Name));
        if (existingRole != null)
        {
            _roles.Remove(existingRole);
        }
    }

    public UserVerificationTokenEntity GenerateVerificationToken(UserVerificationTokenType type)
    {
        var token = new UserVerificationTokenEntity(type);
        _verificationToken.Add(token);
        return token;
    }

    public bool RemoveVerificationToken(UserVerificationTokenEntity token)
    {
        return _verificationToken.Remove(token);
    }

    public void UpdateContactInfo(string email, string phoneNumber)
    {
        SetEmail(new Email(email));
        SetPhoneNumber(new PhoneNumber(phoneNumber));
    }
}