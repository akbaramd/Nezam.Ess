using Bonyan.Layer.Domain.Services;
using Microsoft.Extensions.Logging;
using Nezam.Modular.ESS.Identity.Domain.Roles;
using Nezam.Modular.ESS.Identity.Domain.User;

namespace Nezam.Modular.ESS.Identity.Domain.DomainServices;

public class UserManager : DomainService
{
    public IRoleRepository RoleRepository => LazyServiceProvider.LazyGetRequiredService<IRoleRepository>();
    public IUserRepository UserRepository => LazyServiceProvider.LazyGetRequiredService<IUserRepository>();

    public async Task<bool> CreateAsync(UserEntity entity, string password)
    {
        try
        {
            if (await UserRepository.ExistsAsync(x => x.UserName.Equals(entity.UserName)))
            {
                return false;
            }

            entity.SetPassword(password);
            var res = await UserRepository.AddAsync(entity, true);
            return true;
        }
        catch (Exception e)
        {
            Logger.LogError(e.Message);
            return false;
        }
    }

    public async Task<bool> UpdateAsync(UserEntity entity)
    {
        try
        {
            await UserRepository.UpdateAsync(entity, true);
            return true;
        }
        catch (Exception e)
        {
            Logger.LogError(e.Message);
            return false;
        }
    }

    public async Task<UserEntity?> FindByUserNameAsync(string userName)
    {
        try
        {
            return await UserRepository.FindOneAsync(x => x.UserName.Equals(userName));
        }
        catch (Exception e)
        {
            Logger.LogError(e.Message);
            return null;
        }
    }

    public async Task<bool> AssignRoles(UserEntity entity, params string[] roles)
    {
        try
        {
            foreach (var role in roles)
            {
                var findRole = await RoleRepository.FindOneAsync(x => x.Name.Equals(role));
                if (findRole != null) entity.TryAssignRole(findRole);
            }
            await UserRepository.UpdateAsync(entity, true);
            return true;
        }
        catch (Exception e)
        {
            Logger.LogError(e.Message);
            return false;
        }
    }

    public void RemoveRoles(string[] roles)
    {
        throw new NotImplementedException();
    }

    public async Task UpdateRolesAsync(UserEntity user, string[] roles)
    {
        try
        {
            // Get current roles assigned to the user
            var currentRoles = user.Roles.Select(r => r.Name).ToHashSet();

            // Determine roles to remove (existing roles not in the new roles set)
            var rolesToRemove = currentRoles.Except(roles).ToArray();

            // Remove roles
            foreach (var roleName in rolesToRemove)
            {
                var role = await RoleRepository.FindOneAsync(x => x.Name.Equals(roleName));
                if (role != null)
                {
                    user.TryRemoveRole(role);
                }
            }

            // Determine roles to add (new roles not currently assigned)
            var rolesToAdd = roles.Except(currentRoles).ToArray();

            // Add new roles
            foreach (var roleName in rolesToAdd)
            {
                var role = await RoleRepository.FindOneAsync(x => x.Name.Equals(roleName));
                if (role != null)
                {
                    user.TryAssignRole(role);
                }
            }

            // Update the user in the repository
            await UserRepository.UpdateAsync(user, true);
        }
        catch (Exception e)
        {
            Logger.LogError(e, "Error updating roles for user.");
            throw new Exception("An error occurred while updating roles.", e);
        }
    }
}
