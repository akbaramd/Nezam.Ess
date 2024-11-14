using Bonyan.Layer.Application.Services;
using Bonyan.Layer.Domain.Model;
using Bonyan.UserManagement.Domain.ValueObjects;
using Nezam.Modular.ESS.Identity.Application.Roles.Dto;
using Nezam.Modular.ESS.Identity.Application.Users.Dto;

namespace Nezam.Modular.ESS.Identity.Application.Users;

public interface IUserService : IBonApplicationService
{
    Task<BonPaginatedResult<UserDtoWithDetail>> GetBonPaginatedResult(UserFilterDto filterDto);
    Task<UserDtoWithDetail> GetUserByIdAsync(BonUserId BonUserId);
    Task<UserDtoWithDetail> UpdateUserAsync(BonUserId BonUserId, UserUpdateDto updateUserDto);
    Task<bool> DeleteUserAsync(BonUserId BonUserId);
}