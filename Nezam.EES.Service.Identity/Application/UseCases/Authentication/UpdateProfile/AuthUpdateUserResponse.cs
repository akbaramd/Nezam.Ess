namespace Nezam.EES.Service.Identity.Application.UseCases.Authentication;

public class AuthUpdateUserResponse
{
    public Guid UserId { get; set; }
    public string Message { get; set; } = "User updated successfully.";
}