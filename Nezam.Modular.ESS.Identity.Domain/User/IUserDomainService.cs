using Payeh.SharedKernel.Results;

namespace Nezam.Modular.ESS.Identity.Domain.User;

public interface IUserDomainService
{
    Task<PayehResult> AssignRoleAsync(UserEntity user, params RoleId[] roles);
    Task<PayehResult> UnassignRole(UserEntity user, RoleId role);
    Task<PayehResult> UpdateRoles(UserEntity user, params RoleId[] roles);
    Task<PayehResult<UserEntity>> Create(UserEntity user);
    Task<PayehResult> UpdateAsync(UserEntity user);
    Task<PayehResult> DeleteAsync(UserEntity user);
    Task<PayehResult<UserEntity>> GetUserByIdAsync(UserId userId);
    Task<PayehResult<UserEntity>> GetUserByUsernameAsync(UserNameValue username);
}