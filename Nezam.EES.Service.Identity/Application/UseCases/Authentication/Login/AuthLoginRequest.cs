namespace Nezam.EES.Service.Identity.Application.UseCases.Authentication;

public class AuthLoginRequest
{
    public string UserName { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}