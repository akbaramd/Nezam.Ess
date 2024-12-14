using Nezam.EES.Service.Identity.Domains.Roles;
using Nezam.EEs.Shared.Domain.Identity.Roles;

namespace Nezam.EES.Service.Identity.Application.Dto.Roles;

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