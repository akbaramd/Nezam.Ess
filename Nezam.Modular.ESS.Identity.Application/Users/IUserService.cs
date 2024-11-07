using Bonyan.Layer.Application.Services;
using Bonyan.Layer.Domain.Model;
using Bonyan.UserManagement.Domain.ValueObjects;
using Nezam.Modular.ESS.IdEntity.Application.Roles.Dto;
using Nezam.Modular.ESS.IdEntity.Application.Users.Dto;

namespace Nezam.Modular.ESS.IdEntity.Application.Users;

public interface IUserService : IBonApplicationService
{
    Task<BonPaginatedResult<UserDtoWithDetail>> GetBonPaginatedResult(UserFilterDto filterDto);
    Task<UserDtoWithDetail> GetUserByIdAsync(BonUserId BonUserId);
    Task<UserDtoWithDetail> UpdateUserAsync(BonUserId BonUserId, UserUpdateDto updateUserDto);
    Task<bool> DeleteUserAsync(BonUserId BonUserId);
    Task<List<RoleDto>> GetUserRolesAsync(BonUserId BonUserId);
}