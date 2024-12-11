using Bonyan.Layer.Domain.DomainService;
using Nezam.Modular.ESS.Identity.Domain.Roles;
using Nezam.Modular.ESS.Identity.Domain.Shared.Roles;
using Nezam.Modular.ESS.Identity.Domain.Shared.User;

namespace Nezam.Modular.ESS.Identity.Domain.User
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
        public async Task<BonDomainResult> AssignRoleAsync(UserEntity user, params RoleId[] roles)
        {
            if (user == null || roles == null || roles.Length == 0)
                return BonDomainResult.Failure("User or roles cannot be null or empty.");

            bool isUpdated = false;  // Flag to track if there were any changes

            foreach (var roleId in roles)
            {
                var role = await _roleRepository.GetRoleByIdAsync(roleId);
                if (role == null)
                    return BonDomainResult.Failure($"Role with ID {roleId} not found.");

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

            return BonDomainResult.Success();
        }


        // Unassign role from the user
        public async Task<BonDomainResult> UnassignRole(UserEntity user, RoleId role)
        {
            if (user == null || role == null)
                return BonDomainResult.Failure("User or role cannot be null.");

            var roleEntity = await _roleRepository.GetRoleByIdAsync(role);
            if (roleEntity == null)
                return BonDomainResult.Failure("Role not found.");

            if (!user.Roles.Any(r => r.RoleId == role))
                return BonDomainResult.Failure("User does not have this role.");

            user.RemoveRole(roleEntity); // Remove the role from the user
            await _userRepository.UpdateAsync(user, true); // Save the updated user (with auto-save flag)
            return BonDomainResult.Success();
        }

        // Update user roles
        public async Task<BonDomainResult> UpdateRoles(UserEntity user, params RoleId[] roles)
        {
            if (user == null || roles == null || roles.Length == 0)
                return BonDomainResult.Failure("User or roles cannot be null or empty.");

            bool isUpdated = false;  // Flag to track if there were any changes

            // Clear existing roles and add the new ones
            var currentRoleIds = user.Roles.Select(r => r.RoleId).ToHashSet();
            var newRoleIds = roles.ToHashSet();

            // If the roles haven't changed, skip the update
            if (currentRoleIds.SetEquals(newRoleIds))
            {
                return BonDomainResult.Success();
            }

            user.Roles.Clear(); // Clear existing roles

            foreach (var roleId in roles)
            {
                var role = await _roleRepository.GetRoleByIdAsync(roleId);
                if (role == null)
                    return BonDomainResult.Failure($"Role with ID {roleId} not found.");

                user.AddRole(role);
                isUpdated = true; // Mark as updated
            }

            // Only update the user if there were changes
            if (isUpdated)
            {
                await _userRepository.UpdateAsync(user, true); // Save the updated user (with auto-save flag)
            }

            return BonDomainResult.Success();
        }


        // Create a new user
        public async Task<BonDomainResult<UserEntity>> Create(UserEntity user)
        {
            if (user == null)
                return BonDomainResult<UserEntity>.Failure("User cannot be null.");

            await _userRepository.AddAsync(user, true); // Save the new user (with auto-save flag)
            return BonDomainResult<UserEntity>.Success(user);
        }

        // Update an existing user
        public async Task<BonDomainResult> UpdateAsync(UserEntity user)
        {
            if (user == null)
                return BonDomainResult.Failure("User cannot be null.");

            await _userRepository.UpdateAsync(user, true); // Save the updated user (with auto-save flag)
            return BonDomainResult.Success();
        }

        // Delete a user
        public async Task<BonDomainResult> DeleteAsync(UserEntity user)
        {
            if (user == null)
                return BonDomainResult.Failure("User cannot be null.");

            await _userRepository.DeleteAsync(user, true); // Delete the user (with auto-save flag)
            return BonDomainResult.Success();
        }

        // Get a user by ID
        public async Task<BonDomainResult<UserEntity>> GetUserByIdAsync(UserId userId)
        {
            var user = await _userRepository.GetByUserIdAsync(userId);
            if (user == null)
                return BonDomainResult<UserEntity>.Failure("User not found.");

            return BonDomainResult<UserEntity>.Success(user);
        }

        // Get a user by username
        public async Task<BonDomainResult<UserEntity>> GetUserByUsernameAsync(UserNameValue username)
        {
            var user = await _userRepository.GetByUserNameAsync(username);
            if (user == null)
                return BonDomainResult<UserEntity>.Failure("User not found.");

            return BonDomainResult<UserEntity>.Success(user);
        }
    }
}
