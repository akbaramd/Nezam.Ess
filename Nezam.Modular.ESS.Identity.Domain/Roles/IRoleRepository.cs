using Nezam.Modular.ESS.Identity.Domain.Shared.Roles;
using Payeh.SharedKernel.Domain.Repositories;

namespace Nezam.Modular.ESS.Identity.Domain.Roles;

public interface IRoleRepository : IRepository<RoleEntity>
{
    Task<RoleEntity> GetRoleByIdAsync(RoleId roleId);
}