using Bonyan.Layer.Domain.Repository.Abstractions;
using Nezam.Modular.ESS.Identity.Domain.Shared.Roles;

namespace Nezam.Modular.ESS.Identity.Domain.Roles;

public interface IRoleRepository : IBonRepository<RoleEntity>
{
    Task<RoleEntity> GetRoleByIdAsync(RoleId roleId);
}