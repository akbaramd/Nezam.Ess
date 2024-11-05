using Bonyan.Layer.Application.Services;
using Bonyan.Layer.Domain.Model;
using Bonyan.UserManagement.Domain.ValueObjects;
using Nezam.Modular.ESS.Identity.Application.Roles.Dto;
using Nezam.Modular.ESS.Identity.Application.Users.Dto;

namespace Nezam.Modular.ESS.Identity.Application.Users;

public interface IUserService : IApplicationService
{
    Task<PaginatedResult<UserDtoWithDetail>> GetPaginatedResult(UserFilterDto filterDto);
    Task<UserDtoWithDetail> GetUserByIdAsync(UserId userId);
    Task<UserDtoWithDetail> UpdateUserAsync(UserId userId, UserUpdateDto updateUserDto);
    Task<bool> DeleteUserAsync(UserId userId);
    Task<List<RoleDto>> GetUserRolesAsync(UserId userId);
}