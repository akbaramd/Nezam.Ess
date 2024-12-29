using Nezam.EES.Service.Identity.Application.Dto.Roles;
using Nezam.EES.Service.Identity.Domains.Users;
using Nezam.EEs.Shared.Domain.Identity.User;
using Nezam.EEs.Shared.Domain.Identity.User.ValueObjects;

namespace Nezam.EES.Service.Identity.Application.Dto.Users;

public class UserDto
{
    public UserId UserId { get; set; }
    public UserNameId UserName { get; set; } 
    public UserEmailValue? Email { get; set; } 
    public UserProfileValue Profile { get; set; } // Optional profile info
    public List<RoleDto> Roles { get; set; } // Optional profile info

    // Static method for mapping UserEntity to UserDto
    public static UserDto FromEntity(UserEntity user)
    {
        return new UserDto
        {
            UserId = user.UserId,
            UserName = user.UserName, // Assuming UserName is a value object
            Email = user.Email,      // Assuming Email is a value object
            Profile = user.Profile,
            Roles = user.Roles.Select(x => RoleDto.FromEntity(x)).ToList()
        };
    }
}