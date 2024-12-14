using Nezam.EEs.Shared.Domain.Identity.Roles;
using Payeh.SharedKernel.Results;

namespace Nezam.EES.Service.Identity.Domains.Roles.DomainServices;

public interface IRoleDomainService
{
    Task<PayehResult<RoleEntity>> CreateRoleAsync(RoleEntity role);
    Task<PayehResult> UpdateRoleAsync(RoleEntity role);
    Task<PayehResult> DeleteRoleAsync(RoleEntity role);
    Task<PayehResult<RoleEntity>> GetRoleByIdAsync(RoleId roleId);
    Task<PayehResult<IEnumerable<RoleEntity>>> GetAllRolesAsync();
}