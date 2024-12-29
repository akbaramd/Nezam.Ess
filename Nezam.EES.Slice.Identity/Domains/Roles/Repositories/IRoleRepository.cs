using Nezam.EEs.Shared.Domain.Identity.Roles;
using Payeh.SharedKernel.Domain.Repositories;

namespace Nezam.EES.Service.Identity.Domains.Roles.Repositories;

public interface IRoleRepository : IRepository<RoleEntity>
{
    Task<RoleEntity?> FindRoleByIdAsync(RoleId roleId);
}