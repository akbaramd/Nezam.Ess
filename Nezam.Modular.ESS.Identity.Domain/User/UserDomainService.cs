using Bonyan.Layer.Domain.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Nezam.Modular.ESS.Identity.Domain.Roles;
using Nezam.Modular.ESS.Identity.Domain.Shared.Roles;

namespace Nezam.Modular.ESS.Identity.Domain.User
{
    public class UserDomainService : BonDomainService
    {
        public IRoleRepository RoleRepository => LazyServiceProvider.LazyGetRequiredService<IRoleRepository>();
        public IUserRepository UserRepository => LazyServiceProvider.LazyGetRequiredService<IUserRepository>();

        // Create a new user and set the initial password
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

        // Find a user by username
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

        // Change user's password
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

        // Reset user's password (for admin use cases)
        public async Task<bool> ResetPasswordAsync(UserEntity entity, string newPassword)
        {
            entity.SetPassword(newPassword);
            return await UpdateAsync(entity);
        }

        // Assign multiple roles to a user using role names
        public async Task<bool> AssignRolesAsync(UserEntity entity, params RoleId[] roleNames)
        {
            try
            {
                // Fetch existing role names from the repository
                var existingRoleNames = await RoleRepository.Queryable
                    .Where(r => roleNames.Contains(r.Id))
                    .Select(r => r.Id)
                    .ToListAsync();

                // Assign roles that exist
                foreach (var roleName in existingRoleNames)
                {
                  
                    entity.AssignRole(roleName);
                }

                // Log warnings for roles that do not exist
                var nonExistingRoleNames = roleNames.Except(existingRoleNames);
                foreach (var roleName in nonExistingRoleNames)
                {
                    Logger.LogWarning($"Role with name {roleName} does not exist.");
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

        // Remove multiple roles from a user using role names
        public async Task<bool> RemoveRolesAsync(UserEntity entity, params RoleId[] roleNames)
        {
            try
            {
                // Fetch existing role names from the repository
                var existingRoleNames = await RoleRepository.Queryable
                    .Where(r => roleNames.Contains(r.Id))
                    .Select(r => r.Id)
                    .ToListAsync();

                // Remove roles that exist
                foreach (var roleId in existingRoleNames)
                {
                
                    entity.RemoveRole(roleId);
                }

                // Log warnings for roles that do not exist
                var nonExistingRoleNames = roleNames.Except(existingRoleNames);
                foreach (var roleName in nonExistingRoleNames)
                {
                    Logger.LogWarning($"Role with name {roleName} does not exist.");
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

        // Synchronize user's roles using a list of role names
        public async Task UpdateRolesAsync(UserEntity user, RoleId[] roleNames)
        {
            try
            {
                // Fetch all existing role names
                var existingRoleNames = await RoleRepository.Queryable
                    .Select(r => r.Id)
                    .ToListAsync();

                // Filter valid role names
                var validRoleNames = roleNames.Intersect(existingRoleNames).ToList();

                // Log warnings for roles that do not exist
                var nonExistingRoleNames = roleNames.Except(validRoleNames);
                foreach (var roleName in nonExistingRoleNames)
                {
                    Logger.LogWarning($"Role with name {roleName} does not exist.");
                }

                // Current roles assigned to the user
                var currentRoleNames = user.Roles.Select(rid => rid.RoleId).ToHashSet();

                // Determine roles to add and remove
                var rolesToAdd = validRoleNames.Except(currentRoleNames);
                var rolesToRemove = currentRoleNames.Except(validRoleNames);

                // Add new roles
                foreach (var roleName in rolesToAdd)
                {
                    user.AssignRole(roleName);
                }

                // Remove roles no longer assigned
                foreach (var roleName in rolesToRemove)
                {
                    user.RemoveRole( roleName);
                }

                await UserRepository.UpdateAsync(user, true);
            }
            catch (Exception e)
            {
                Logger.LogError(e, "Error updating roles for user.");
                throw new Exception("An error occurred while updating roles.", e);
            }
        }

        // Check if the user has a specific role by name
        public async Task<bool> HasRoleAsync(UserEntity user, RoleId roleName)
        {
            // Since RoleId is based on role name, we can directly check
            var roleExists = await RoleRepository.ExistsAsync(r => r.Id.Equals(roleName));
            if (!roleExists)
            {
                Logger.LogWarning($"Role with name {roleName} does not exist.");
                return false;
            }

            return user.Roles.Any(rid => rid.RoleId == roleName);
        }
    }
}
