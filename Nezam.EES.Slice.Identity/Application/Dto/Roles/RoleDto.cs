using Nezam.EES.Service.Identity.Domains.Roles;
using Nezam.EEs.Shared.Domain.Identity.Roles;

namespace Nezam.EES.Service.Identity.Application.Dto.Roles;

public class RoleDto
{
    public string RoleId { get; set; } // Using raw GUID for external compatibility
    public string Title { get; set; } = string.Empty; // Title of the role

    public static RoleDto FromEntity(RoleEntity role)
    {
        if (role == null) throw new ArgumentNullException(nameof(role));

        return new RoleDto
        {
            RoleId = role.RoleId.Value,
            Title = role.Title
        };
    }
}