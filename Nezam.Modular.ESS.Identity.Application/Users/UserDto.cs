using Nezam.Modular.ESS.Identity.Domain.Shared.User;
using Nezam.Modular.ESS.Identity.Domain.User;

namespace Nezam.Modular.ESS.Identity.Application.Users;

public class UserDto
{
    public UserId UserId { get; set; }
    public UserNameValue UserName { get; set; } 
    public UserEmailValue? Email { get; set; } 
    public UserProfileValue Profile { get; set; } // Optional profile info

    // Static method for mapping UserEntity to UserDto
    public static UserDto FromEntity(UserEntity user)
    {
        return new UserDto
        {
            UserId = user.UserId,
            UserName = user.UserName, // Assuming UserName is a value object
            Email = user.Email,      // Assuming Email is a value object
            Profile = user.Profile
        };
    }
}