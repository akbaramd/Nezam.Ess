using Nezam.EES.Service.Identity.Domains.Roles.Repositories;
using Nezam.EES.Service.Identity.Domains.Users.Repositories;
using Nezam.EEs.Shared.Domain.Identity.Roles;
using Nezam.EEs.Shared.Domain.Identity.User;
using Payeh.SharedKernel.Results;

namespace Nezam.EES.Service.Identity.Domains.Users.DomainServices
{
    public class UserDomainService : IUserDomainService
    {
        private readonly IUserRepository _userRepository; // Dependency for fetching and saving users
        private readonly IRoleRepository _roleRepository; // Dependency for fetching roles

        public UserDomainService(IUserRepository userRepository, IRoleRepository roleRepository)
        {
            _userRepository = userRepository;
            _roleRepository = roleRepository;
        }

        // Assign role(s) to the user
        public async Task<PayehResult> AssignRoleAsync(UserEntity user, params RoleId[] roles)
        {
            if (user == null || roles == null || roles.Length == 0)
                return PayehResult.Failure("User or roles cannot be null or empty.");

            bool isUpdated = false;  // Flag to track if there were any changes

            foreach (var roleId in roles)
            {
                var role = await _roleRepository.FindRoleByIdAsync(roleId);
                if (role == null)
                    return PayehResult.Failure($"Role with ID {roleId} not found.");

                if (user.Roles.Any(r => r.RoleId == roleId))
                    continue; // Skip if user already has the role

                user.AddRole(role); // Add the role to the user
                isUpdated = true; // Mark as updated
            }

            // Only update the user if roles have been added
            if (isUpdated)
            {
                await _userRepository.UpdateAsync(user, true); // Save the updated user (with auto-save flag)
            }

            return PayehResult.Success();
        }


        // Unassign role from the user
        public async Task<PayehResult> UnassignRole(UserEntity user, RoleId role)
        {
            if (user == null || role == null)
                return PayehResult.Failure("User or role cannot be null.");

            var roleEntity = await _roleRepository.FindRoleByIdAsync(role);
            if (roleEntity == null)
                return PayehResult.Failure("Role not found.");

            if (!user.Roles.Any(r => r.RoleId == role))
                return PayehResult.Failure("User does not have this role.");

            user.RemoveRole(roleEntity); // Remove the role from the user
            await _userRepository.UpdateAsync(user, true); // Save the updated user (with auto-save flag)
            return PayehResult.Success();
        }

        // Update user roles
        public async Task<PayehResult> UpdateRoles(UserEntity user, params RoleId[] roles)
        {
            if (user == null || roles == null || roles.Length == 0)
                return PayehResult.Failure("User or roles cannot be null or empty.");

            bool isUpdated = false;  // Flag to track if there were any changes

            // Clear existing roles and add the new ones
            var currentRoleIds = user.Roles.Select(r => r.RoleId).ToHashSet();
            var newRoleIds = roles.ToHashSet();

            // If the roles haven't changed, skip the update
            if (currentRoleIds.SetEquals(newRoleIds))
            {
                return PayehResult.Success();
            }

            user.Roles.Clear(); // Clear existing roles

            foreach (var roleId in roles)
            {
                var role = await _roleRepository.FindRoleByIdAsync(roleId);
                if (role == null)
                    return PayehResult.Failure($"Role with ID {roleId} not found.");

                user.AddRole(role);
                isUpdated = true; // Mark as updated
            }

            // Only update the user if there were changes
            if (isUpdated)
            {
                await _userRepository.UpdateAsync(user, true); // Save the updated user (with auto-save flag)
            }

            return PayehResult.Success();
        }


        // Create a new user
        public async Task<PayehResult<UserEntity>> Create(UserEntity user)
        {
            if (user == null)
                return PayehResult<UserEntity>.Failure("User cannot be null.");

            await _userRepository.AddAsync(user, true); // Save the new user (with auto-save flag)
            return PayehResult<UserEntity>.Success(user);
        }

        // Update an existing user
        public async Task<PayehResult> UpdateAsync(UserEntity user)
        {
            if (user == null)
                return PayehResult.Failure("User cannot be null.");

            await _userRepository.UpdateAsync(user, true); // Save the updated user (with auto-save flag)
            return PayehResult.Success();
        }

        // Delete a user
        public async Task<PayehResult> DeleteAsync(UserEntity user)
        {
            if (user == null)
                return PayehResult.Failure("User cannot be null.");

            await _userRepository.DeleteAsync(user, true); // Delete the user (with auto-save flag)
            return PayehResult.Success();
        }

        // Get a user by ID
        public async Task<PayehResult<UserEntity>> GetUserByIdAsync(UserId userId)
        {
            var user = await _userRepository.GetByUserIdAsync(userId);
            if (user == null)
                return PayehResult<UserEntity>.Failure("User not found.");

            return PayehResult<UserEntity>.Success(user);
        }

        // Get a user by username
        public async Task<PayehResult<UserEntity>> GetUserByUsernameAsync(UserNameId username)
        {
            var user = await _userRepository.GetByUserNameAsync(username);
            if (user == null)
                return PayehResult<UserEntity>.Failure("User not found.");

            return PayehResult<UserEntity>.Success(user);
        }
    }
}
