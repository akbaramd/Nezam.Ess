using Bonyan.Layer.Domain.Aggregates;
using Bonyan.Layer.Domain.ValueObjects;
using Bonyan.UserManagement.Domain;
using Bonyan.UserManagement.Domain.ValueObjects;
using Nezam.Modular.ESS.Identity.Domain.Roles;

namespace Nezam.Modular.ESS.Identity.Domain.User;

public class UserEntity : BonyanUser
{
    protected UserEntity(){}

    public UserEntity(UserId userId, string userName)
    {
        Id = userId;
        UserName = userName;
    }
    
    private readonly List<RoleEntity> _roles = new List<RoleEntity>();
    public IReadOnlyCollection<RoleEntity> Users => _roles.AsReadOnly();
}

