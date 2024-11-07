using FastEndpoints;
using Nezam.Modular.ESS.IdEntity.Application.Auth;
using Nezam.Modular.ESS.IdEntity.Application.Users.Dto;

namespace Nezam.Modular.ESS.WebApi.Endpoints.Authentication;

public class AuthProfileEndpoint : EndpointWithoutRequest<UserDtoWithDetail>
{
    private readonly IAuthService _currentUser;

    public AuthProfileEndpoint(IAuthService currentUser)
    {
        _currentUser = currentUser;
    }


    public override void Configure()
    {
        Get("/api/auth/profile");

        Description(c=>{
                c.WithTags("Authentication");
        });
    }

    public override async Task HandleAsync( CancellationToken ct)
    {
        var profile = await _currentUser.CurrentUserProfileAsync(
            ct);
        await SendAsync(profile, cancellation: ct);
    }
}