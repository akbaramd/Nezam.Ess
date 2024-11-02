using Bonyan.Layer.Domain.Aggregates;
using Bonyan.Layer.Domain.ValueObjects;
using Bonyan.UserManagement.Domain;
using Bonyan.UserManagement.Domain.ValueObjects;
using Nezam.Modular.ESS.Identity.Domain.Employer;
using Nezam.Modular.ESS.Identity.Domain.Roles;

namespace Nezam.Modular.ESS.Identity.Domain.User;

public class UserEntity : BonyanUser
{
    protected UserEntity(){}

    public UserEntity(UserId userId, string userName):base(userId,userName)
    {
    }
    
    private readonly List<RoleEntity> _roles = new List<RoleEntity>();
    public IReadOnlyCollection<RoleEntity> Roles => _roles.AsReadOnly();


    public void TryAssignRole(RoleEntity role)
    {
        if (!Roles.Any(x=>x.Name.Equals(role.Name)))
        {
            _roles.Add(role);
        }
    }
    
}

