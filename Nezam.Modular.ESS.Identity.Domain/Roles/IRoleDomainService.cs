using Payeh.SharedKernel.Results;

namespace Nezam.Modular.ESS.Identity.Domain.Roles;

public interface IRoleDomainService
{
    Task<PayehResult<RoleEntity>> CreateRoleAsync(RoleEntity role);
    Task<PayehResult> UpdateRoleAsync(RoleEntity role);
    Task<PayehResult> DeleteRoleAsync(RoleEntity role);
    Task<PayehResult<RoleEntity>> GetRoleByIdAsync(RoleId roleId);
    Task<PayehResult<IEnumerable<RoleEntity>>> GetAllRolesAsync();
}