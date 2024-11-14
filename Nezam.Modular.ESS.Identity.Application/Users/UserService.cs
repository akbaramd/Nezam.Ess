using Bonyan.Layer.Application.Services;
using Bonyan.Layer.Domain.Model;
using Bonyan.UserManagement.Domain.ValueObjects;
using Nezam.Modular.ESS.Identity.Application.Roles.Dto;
using Nezam.Modular.ESS.Identity.Application.Users.Dto;
using Nezam.Modular.ESS.Identity.Application.Users.Specs;
using Nezam.Modular.ESS.Identity.Domain.Roles;
using Nezam.Modular.ESS.Identity.Domain.Shared.User;
using Nezam.Modular.ESS.Identity.Domain.User;

namespace Nezam.Modular.ESS.Identity.Application.Users;

public class UserService : BonApplicationService, IUserService
{
    public IUserRepository UserRepository => LazyServiceProvider.LazyGetRequiredService<IUserRepository>();
    public IRoleRepository RoleRepository => LazyServiceProvider.LazyGetRequiredService<IRoleRepository>();
    public UserDomainService UserDomainService => LazyServiceProvider.LazyGetRequiredService<UserDomainService>();
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

        // Update roles via UserDomainService to handle encapsulation and business logic
        if (updateUserDto.RolesIds != null && updateUserDto.RolesIds.Length > 0)
        {
            await UserDomainService.UpdateRolesAsync(user, updateUserDto.RolesIds);
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

   
    // Additional Methods Using UserDomainService and RoleManager


    // Resets a user's password
    public async Task<bool> ResetPasswordAsync(BonUserId BonUserId, string newPassword)
    {
        var user = await UserRepository.GetByIdAsync(BonUserId);
        if (user == null) throw new Exception("User not found");

        return await UserDomainService.ResetPasswordAsync(user, newPassword);
    }
}
