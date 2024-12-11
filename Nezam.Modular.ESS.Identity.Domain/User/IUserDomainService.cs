using Bonyan.Layer.Domain.DomainService;
using Nezam.Modular.ESS.Identity.Domain.Shared.Roles;
using Nezam.Modular.ESS.Identity.Domain.Shared.User;

namespace Nezam.Modular.ESS.Identity.Domain.User;

public interface IUserDomainService
{
    Task<BonDomainResult> AssignRoleAsync(UserEntity user, params RoleId[] roles);
    Task<BonDomainResult> UnassignRole(UserEntity user, RoleId role);
    Task<BonDomainResult> UpdateRoles(UserEntity user, params RoleId[] roles);
    Task<BonDomainResult<UserEntity>> Create(UserEntity user);
    Task<BonDomainResult> UpdateAsync(UserEntity user);
    Task<BonDomainResult> DeleteAsync(UserEntity user);
    Task<BonDomainResult<UserEntity>> GetUserByIdAsync(UserId userId);
    Task<BonDomainResult<UserEntity>> GetUserByUsernameAsync(UserNameValue username);
}