using Nezam.EES.Service.Identity.Application.Dto.Roles;
using Nezam.EES.Service.Identity.Domains.Users;
using Nezam.EEs.Shared.Domain.Identity.User;
using Nezam.EEs.Shared.Domain.Identity.User.ValueObjects;

namespace Nezam.EES.Service.Identity.Application.Dto.Users;

public class UserDto
{
    public Guid UserId { get; set; } // Using raw GUID for external compatibility
    public string UserName { get; set; } // Assuming the string value for simplicity in DTOs
    public string? Email { get; set; } // Nullable email as string for external systems
    public string FirstName { get; set; } // Optional profile info as a string representation
    public string? LastName { get; set; } // Optional profile info as a string representation
    public List<RoleDto> Roles { get; set; } = new(); // List of associated roles
    public bool IsCanDelete { get; set; } // Indicates if the user can be deleted

    public static UserDto FromEntity(UserEntity user)
    {
        if (user == null) throw new ArgumentNullException(nameof(user));

        return new UserDto
        {
            UserId = user.UserId.Value,
            UserName = user.UserName.Value,
            Email = user.Email?.Value,
            FirstName = user.Profile?.FirstName??"",
            LastName = user.Profile?.LastName,
            IsCanDelete = user.IsCanDelete,
            Roles = user.Roles.Select(RoleDto.FromEntity).ToList()
        };
    }
}