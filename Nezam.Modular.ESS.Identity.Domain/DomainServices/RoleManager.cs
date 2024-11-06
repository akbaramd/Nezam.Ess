using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bonyan.Layer.Domain.Services;
using Microsoft.Extensions.Logging;
using Nezam.Modular.ESS.Identity.Domain.Roles;
using Nezam.Modular.ESS.Identity.Domain.User;

namespace Nezam.Modular.ESS.Identity.Domain.DomainServices
{
    public class RoleManager : DomainService
    {
        public IRoleRepository RoleRepository => LazyServiceProvider.LazyGetRequiredService<IRoleRepository>();
        public IUserRepository UserRepository => LazyServiceProvider.LazyGetRequiredService<IUserRepository>();

        // Create a new role
        public async Task<bool> CreateAsync(string name, string title)
        {
            try
            {
                if (await RoleRepository.ExistsAsync(x => x.Name.Equals(name)))
                {
                    Logger.LogWarning($"Role with name {name} already exists.");
                    return false;
                }

                var role = new RoleEntity(RoleId.CreateNew(), name, title);
                await RoleRepository.AddAsync(role, true);
                return true;
            }
            catch (Exception e)
            {
                Logger.LogError(e, "Error creating role.");
                return false;
            }
        }

        // Update role details
        public async Task<bool> UpdateAsync(RoleEntity entity)
        {
            try
            {
                await RoleRepository.UpdateAsync(entity, true);
                return true;
            }
            catch (Exception e)
            {
                Logger.LogError(e, "Error updating role.");
                return false;
            }
        }

        // Find a role by its name
        public async Task<RoleEntity?> FindByNameAsync(string name)
        {
            try
            {
                return await RoleRepository.FindOneAsync(x => x.Name.Equals(name));
            }
            catch (Exception e)
            {
                Logger.LogError(e, "Error finding role by name.");
                return null;
            }
        }

        // Delete a role and optionally remove it from all users
        public async Task<bool> DeleteAsync(RoleEntity role, bool removeFromUsers = true)
        {
            try
            {
                if (removeFromUsers)
                {
                    var usersWithRole = await UserRepository.FindAsync(u => u.Roles.Any(r => r.Id == role.Id));
                    foreach (var user in usersWithRole)
                    {
                        user.TryRemoveRole(role);
                        await UserRepository.UpdateAsync(user, true); // Batch update without saving
                    }
                    
                }

                await RoleRepository.DeleteAsync(role, true);
                return true;
            }
            catch (Exception e)
            {
                Logger.LogError(e, "Error deleting role.");
                return false;
            }
        }

        // Get all roles with the list of users assigned to each role
        public async Task<Dictionary<RoleEntity, List<UserEntity>>> GetRolesWithUsersAsync()
        {
            try
            {
                var rolesWithUsers = new Dictionary<RoleEntity, List<UserEntity>>();
                var roles = await RoleRepository.FindAsync(x=>true);

                foreach (var role in roles)
                {
                    var users = await UserRepository.FindAsync(u => u.Roles.Any(r => r.Id == role.Id));
                    rolesWithUsers[role] = users.ToList();
                }

                return rolesWithUsers;
            }
            catch (Exception e)
            {
                Logger.LogError(e, "Error retrieving roles with users.");
                return new Dictionary<RoleEntity, List<UserEntity>>();
            }
        }

        // Check if a role exists by name
        public async Task<bool> RoleExistsAsync(string roleName)
        {
            return await RoleRepository.ExistsAsync(r => r.Name.Equals(roleName, StringComparison.OrdinalIgnoreCase));
        }
    }
}
