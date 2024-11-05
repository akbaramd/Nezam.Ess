using Bonyan.Layer.Application.Services;
using Bonyan.Layer.Domain.Model;
using Bonyan.UserManagement.Domain.ValueObjects;
using Nezam.Modular.ESS.Identity.Application.Roles.Dto;
using Nezam.Modular.ESS.Identity.Application.Users.Dto;
using Nezam.Modular.ESS.Identity.Application.Users.Specs;
using Nezam.Modular.ESS.Identity.Domain.DomainServices;
using Nezam.Modular.ESS.Identity.Domain.Roles;
using Nezam.Modular.ESS.Identity.Domain.User;

namespace Nezam.Modular.ESS.Identity.Application.Users;

public class UserService : ApplicationService, IUserService
{
    public IUserRepository UserRepository => LazyServiceProvider.LazyGetRequiredService<IUserRepository>();
    public IRoleRepository RoleRepository => LazyServiceProvider.LazyGetRequiredService<IRoleRepository>();
    public UserManager UserManager => LazyServiceProvider.LazyGetRequiredService<UserManager>();
    public RoleManager RoleManager => LazyServiceProvider.LazyGetRequiredService<RoleManager>();

    public async Task<PaginatedResult<UserDtoWithDetail>> GetPaginatedResult(UserFilterDto filterDto)
    {
        var res = await UserRepository.PaginatedAsync(new UsersFilterSpec(filterDto));
        return Mapper.Map<PaginatedResult<UserEntity>, PaginatedResult<UserDtoWithDetail>>(res);
    }

    public async Task<UserDtoWithDetail> GetUserByIdAsync(UserId userId)
    {
        var user = await UserRepository.GetOneAsync(new UserByIdSpec(userId));
        return Mapper.Map<UserEntity, UserDtoWithDetail>(user);
    }

    public async Task<UserDtoWithDetail> UpdateUserAsync(UserId userId, UserUpdateDto updateUserDto)
    {
        var user = await UserRepository.GetOneAsync(new UserByIdSpec(userId));
        if (user == null) throw new Exception("User not found");

        // Update user properties
        user.UpdateContactInfo(updateUserDto.Email, updateUserDto.PhoneNumber);

        // Update roles if provided
        if (updateUserDto.Roles != null && updateUserDto.Roles.Length > 0)
        {
            await UserManager.UpdateRolesAsync(user, updateUserDto.Roles);
        }

        await UserRepository.UpdateAsync(user);
        return Mapper.Map<UserEntity, UserDtoWithDetail>(user);
    }

    public async Task<bool> DeleteUserAsync(UserId userId)
    {
        var user = await UserRepository.GetByIdAsync(userId);
        if (user == null) return false;

        await UserRepository.DeleteAsync(user);
        return true;
    }

    public async Task<List<RoleDto>> GetUserRolesAsync(UserId userId)
    {
        var user = await UserRepository.GetByIdAsync(userId);
        if (user == null) throw new Exception("User not found");

        return Mapper.Map<IReadOnlyCollection<RoleEntity>, List<RoleDto>>(user.Roles);
    }
}