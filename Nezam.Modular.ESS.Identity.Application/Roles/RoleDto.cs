using Nezam.Modular.ESS.Identity.Domain.Roles;
using Nezam.Modular.ESS.Identity.Domain.Shared.Roles;

namespace Nezam.Modular.ESS.Identity.Application.Roles;

public class RoleDto
{
    public RoleId RoleId { get; set; }
    public string Title { get; set; } = string.Empty;

    // Static method for mapping RoleEntity to RoleDto
    public static RoleDto FromEntity(RoleEntity role)
    {
        return new RoleDto
        {
            RoleId = role.RoleId,
            Title = role.Title
        };
    }
}