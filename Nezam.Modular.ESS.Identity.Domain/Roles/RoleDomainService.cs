using Payeh.SharedKernel.Results;

namespace Nezam.Modular.ESS.Identity.Domain.Roles;



public class RoleDomainService : IRoleDomainService
{
    private readonly IRoleRepository _roleRepository;

    public RoleDomainService(IRoleRepository roleRepository)
    {
        _roleRepository = roleRepository;
    }

    // Create a new role
    public async Task<PayehResult<RoleEntity>> CreateRoleAsync(RoleEntity role)
    {
        if (role == null)
            return PayehResult<RoleEntity>.Failure("Role cannot be null.");

        var find =  await _roleRepository.FindRoleByIdAsync(role.RoleId);
        if (find != null)
        {
            return PayehResult.Failure("role already exists.");
        }
        await _roleRepository.AddAsync(role, true); // Save the new role (with auto-save flag)
        return PayehResult<RoleEntity>.Success(role);
    }

    // Update an existing role
    public async Task<PayehResult> UpdateRoleAsync(RoleEntity role)
    {
        if (role == null)
            return PayehResult.Failure("Role cannot be null.");

        await _roleRepository.UpdateAsync(role, true); // Save the updated role (with auto-save flag)
        return PayehResult.Success();
    }

    // Delete a role
    public async Task<PayehResult> DeleteRoleAsync(RoleEntity role)
    {
        if (role == null)
            return PayehResult.Failure("Role cannot be null.");

        await _roleRepository.DeleteAsync(role, true); // Delete the role (with auto-save flag)
        return PayehResult.Success();
    }

    // Get a role by ID
    public async Task<PayehResult<RoleEntity>> GetRoleByIdAsync(RoleId roleId)
    {
        var role = await _roleRepository.FindRoleByIdAsync(roleId);
        if (role == null)
            return PayehResult<RoleEntity>.Failure("Role not found.");

        return PayehResult<RoleEntity>.Success(role);
    }

    // Get all roles
    public async Task<PayehResult<IEnumerable<RoleEntity>>> GetAllRolesAsync()
    {
        var roles = await _roleRepository.FindAsync(x=>true);
        return PayehResult<IEnumerable<RoleEntity>>.Success(roles);
    }
}
