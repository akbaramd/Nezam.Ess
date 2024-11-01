using Bonyan.UserManagement.Domain.ValueObjects;

namespace Nezam.Modular.ESS.Identity.Application.Auth.Dto;

public class AuthLoginDto
{
    public string Username { get; set; }
    public string Password { get; set; }
}
public class AuthJwtDto
{
    public string AccessToken { get; set; }
    public UserId UserId { get; set; }
}