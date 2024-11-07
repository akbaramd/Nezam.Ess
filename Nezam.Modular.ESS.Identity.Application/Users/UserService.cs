using Bonyan.Layer.Application.Services;
using Bonyan.Layer.Domain.Model;
using Bonyan.UserManagement.Domain.ValueObjects;
using Nezam.Modular.ESS.IdEntity.Application.Roles.Dto;
using Nezam.Modular.ESS.IdEntity.Application.Users.Dto;
using Nezam.Modular.ESS.IdEntity.Application.Users.Specs;
using Nezam.Modular.ESS.IdEntity.Domain.DomainServices;
using Nezam.Modular.ESS.IdEntity.Domain.Roles;
using Nezam.Modular.ESS.IdEntity.Domain.User;

namespace Nezam.Modular.ESS.IdEntity.Application.Users;

public class UserService : BonApplicationService, IUserService
{
    public IUserRepository UserRepository => LazyServiceProvider.LazyGetRequiredService<IUserRepository>();
    public IRoleRepository RoleRepository => LazyServiceProvider.LazyGetRequiredService<IRoleRepository>();
    public UserManager UserManager => LazyServiceProvider.LazyGetRequiredService<UserManager>();
    public RoleManager RoleManager => LazyServiceProvider.LazyGetRequiredService<RoleManager>();

    // Retrieves paginated user results based on the provided filter
    public async Task<BonPaginatedResult<UserDtoWithDetail>> GetBonPaginatedResult(UserFilterDto filterDto)
    {
        var result = await UserRepository.PaginatedAsync(new UsersFilterSpec(filterDto));
        return Mapper.Map<BonPaginatedResult<UserEntity>, BonPaginatedResult<UserDtoWithDetail>>(result);
    }

    // Gets a user by their ID
    public async Task<UserDtoWithDetail> GetUserByIdAsync(BonUserId BonUserId)
    {
        var user = await UserRepository.GetOneAsync(new UserByIdSpec(BonUserId));
        if (user == null) throw new Exception("User not found");

        return Mapper.Map<UserEntity, UserDtoWithDetail>(user);
    }

    // Updates a user's contact info and roles
    public async Task<UserDtoWithDetail> UpdateUserAsync(BonUserId BonUserId, UserUpdateDto updateUserDto)
    {
        var user = await UserRepository.GetOneAsync(new UserByIdSpec(BonUserId));
        if (user == null) throw new Exception("User not found");

        // Update contact info using encapsulated method
        user.UpdateContactInfo(updateUserDto.Email, updateUserDto.PhoneNumber);

        // Update roles via UserManager to handle encapsulation and business logic
        if (updateUserDto.Roles != null && updateUserDto.Roles.Length > 0)
        {
            await UserManager.UpdateRolesAsync(user, updateUserDto.Roles);
        }

        await UserRepository.UpdateAsync(user);
        return Mapper.Map<UserEntity, UserDtoWithDetail>(user);
    }

    // Deletes a user by their ID
    public async Task<bool> DeleteUserAsync(BonUserId BonUserId)
    {
        var user = await UserRepository.GetByIdAsync(BonUserId);
        if (user == null) return false;

        await UserRepository.DeleteAsync(user);
        return true;
    }

    // Gets the roles assigned to a specific user
    public async Task<List<RoleDto>> GetUserRolesAsync(BonUserId BonUserId)
    {
        var user = await UserRepository.GetByIdAsync(BonUserId);
        if (user == null) throw new Exception("User not found");

        return Mapper.Map<IReadOnlyCollection<RoleEntity>, List<RoleDto>>(user.Roles);
    }

    // Additional Methods Using UserManager and RoleManager


    // Resets a user's password
    public async Task<bool> ResetPasswordAsync(BonUserId BonUserId, string newPassword)
    {
        var user = await UserRepository.GetByIdAsync(BonUserId);
        if (user == null) throw new Exception("User not found");

        return await UserManager.ResetPasswordAsync(user, newPassword);
    }
}
