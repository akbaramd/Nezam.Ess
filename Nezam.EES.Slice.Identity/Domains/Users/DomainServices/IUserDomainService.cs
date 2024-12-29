using Nezam.EEs.Shared.Domain.Identity.Roles;
using Nezam.EEs.Shared.Domain.Identity.User;
using Payeh.SharedKernel.Results;

namespace Nezam.EES.Service.Identity.Domains.Users.DomainServices;

public interface IUserDomainService
{
    Task<PayehResult> AssignRoleAsync(UserEntity user, params RoleId[] roles);
    Task<PayehResult> UnassignRole(UserEntity user, RoleId role);
    Task<PayehResult> UpdateRoles(UserEntity user, params RoleId[] roles);
    Task<PayehResult<UserEntity>> Create(UserEntity user);
    Task<PayehResult> UpdateAsync(UserEntity user);
    Task<PayehResult> DeleteAsync(UserEntity user);
    Task<PayehResult<UserEntity>> GetUserByIdAsync(UserId userId);
    Task<PayehResult<UserEntity>> GetUserByUsernameAsync(UserNameId username);
}