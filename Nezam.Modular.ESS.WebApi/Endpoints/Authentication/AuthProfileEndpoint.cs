using Bonyan.UserManagement.Application.Dtos;
using FastEndpoints;
using Nezam.Modular.ESS.Identity.Application.Auth;

namespace Nezam.Modular.ESS.WebApi.Endpoints.Authentication;

public class AuthProfileEndpoint : EndpointWithoutRequest<BonyanUserDto>
{
    private readonly IAuthService _currentUser;

    public AuthProfileEndpoint(IAuthService currentUser)
    {
        _currentUser = currentUser;
    }


    public override void Configure()
    {
        Get("/api/auth/profile");
    }

    public override async Task HandleAsync( CancellationToken ct)
    {
        var profile = await _currentUser.CurrentUserProfileAsync(
            ct);
        await SendAsync(profile, cancellation: ct);
    }
}