using Bonyan.Layer.Domain.Abstractions;
using Nezam.Modular.ESS.Identity.Domain.Shared.Roles;

namespace Nezam.Modular.ESS.Identity.Domain.Roles;

public interface IRoleRepository : IBonRepository<RoleEntity,RoleId>
{
    
}