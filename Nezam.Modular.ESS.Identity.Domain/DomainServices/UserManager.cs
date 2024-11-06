using System;
using System.Linq;
using System.Threading.Tasks;
using Bonyan.Layer.Domain.Services;
using Microsoft.Extensions.Logging;
using Nezam.Modular.ESS.Identity.Domain.Roles;
using Nezam.Modular.ESS.Identity.Domain.User;

namespace Nezam.Modular.ESS.Identity.Domain.DomainServices
{
    public class UserManager : DomainService
    {
        public IRoleRepository RoleRepository => LazyServiceProvider.LazyGetRequiredService<IRoleRepository>();
        public IUserRepository UserRepository => LazyServiceProvider.LazyGetRequiredService<IUserRepository>();

        // Create a new user and set an initial password
        public async Task<bool> CreateAsync(UserEntity entity, string password)
        {
            try
            {
                if (await UserRepository.ExistsAsync(x => x.UserName.Equals(entity.UserName)))
                {
                    Logger.LogWarning($"User with username {entity.UserName} already exists.");
                    return false;
                }

                entity.SetPassword(password);
                await UserRepository.AddAsync(entity, true);
                return true;
            }
            catch (Exception e)
            {
                Logger.LogError(e, "Error creating user.");
                return false;
            }
        }

        // Update user information
        public async Task<bool> UpdateAsync(UserEntity entity)
        {
            try
            {
                await UserRepository.UpdateAsync(entity, true);
                return true;
            }
            catch (Exception e)
            {
                Logger.LogError(e, "Error updating user.");
                return false;
            }
        }

        // Find user by username
        public async Task<UserEntity?> FindByUserNameAsync(string userName)
        {
            try
            {
                return await UserRepository.FindOneAsync(x => x.UserName.Equals(userName));
            }
            catch (Exception e)
            {
                Logger.LogError(e, "Error finding user by username.");
                return null;
            }
        }

        // Change a user's password
        public async Task<bool> ChangePasswordAsync(UserEntity entity, string currentPassword, string newPassword)
        {
            if (!entity.VerifyPassword(currentPassword))
            {
                Logger.LogWarning("Current password does not match.");
                return false;
            }

            entity.SetPassword(newPassword);
            return await UpdateAsync(entity);
        }

        // Reset a user's password directly (for admin use cases)
        public async Task<bool> ResetPasswordAsync(UserEntity entity, string newPassword)
        {
            entity.SetPassword(newPassword);
            return await UpdateAsync(entity);
        }

        // Assign multiple roles to a user
        public async Task<bool> AssignRolesAsync(UserEntity entity, params string[] roles)
        {
            try
            {
                foreach (var role in roles)
                {
                    var findRole = await RoleRepository.FindOneAsync(x => x.Name.Equals(role));
                    if (findRole != null)
                    {
                        entity.TryAssignRole(findRole);
                    }
                }

                await UserRepository.UpdateAsync(entity, true);
                return true;
            }
            catch (Exception e)
            {
                Logger.LogError(e, "Error assigning roles to user.");
                return false;
            }
        }

        // Remove multiple roles from a user
        public async Task<bool> RemoveRolesAsync(UserEntity entity, params string[] roles)
        {
            try
            {
                foreach (var roleName in roles)
                {
                    var role = await RoleRepository.FindOneAsync(x => x.Name.Equals(roleName));
                    if (role != null)
                    {
                        entity.TryRemoveRole(role);
                    }
                }

                await UserRepository.UpdateAsync(entity, true);
                return true;
            }
            catch (Exception e)
            {
                Logger.LogError(e, "Error removing roles from user.");
                return false;
            }
        }

        // Update roles assigned to a user, syncing with the provided list of roles
        public async Task UpdateRolesAsync(UserEntity user, string[] roles)
        {
            try
            {
                var currentRoles = user.Roles.Select(r => r.Name).ToHashSet();
                var rolesToRemove = currentRoles.Except(roles).ToArray();
                var rolesToAdd = roles.Except(currentRoles).ToArray();

                // Remove roles not in the new list
                foreach (var roleName in rolesToRemove)
                {
                    var role = await RoleRepository.FindOneAsync(x => x.Name.Equals(roleName));
                    if (role != null)
                    {
                        user.TryRemoveRole(role);
                    }
                }

                // Add roles that are in the new list but not currently assigned
                foreach (var roleName in rolesToAdd)
                {
                    var role = await RoleRepository.FindOneAsync(x => x.Name.Equals(roleName));
                    if (role != null)
                    {
                        user.TryAssignRole(role);
                    }
                }

                await UserRepository.UpdateAsync(user, true);
            }
            catch (Exception e)
            {
                Logger.LogError(e, "Error updating roles for user.");
                throw new Exception("An error occurred while updating roles.", e);
            }
        }

        // Check if a user has a specific role
        public bool HasRole(UserEntity user, string roleName)
        {
            return user.Roles.Any(role => role.Name.Equals(roleName, StringComparison.OrdinalIgnoreCase));
        }
    }
}
