using Bonyan.ExceptionHandling;
using FastEndpoints;
using Nezam.Modular.ESS.IdEntity.Application.Auth;
using Nezam.Modular.ESS.IdEntity.Application.Auth.Dto;

namespace Nezam.Modular.ESS.WebApi.Endpoints.Authentication;

public class AuthLoginEndpoint : Endpoint<AuthLoginDto,AuthJwtResult>
{
    private readonly IAuthService _authService;

    public AuthLoginEndpoint(IAuthService authService)
    {
        _authService = authService;
    }

    public override void Configure()
    {
        Post("/api/auth/login");
      
        Description(c =>
        {
            c.Produces<AuthJwtResult>(200);
            c.Produces<HttpExceptionModel>(500);
                c.WithTags("Authentication");
        });
        Summary(c =>
        {
            c.ExampleRequest = new AuthLoginDto()
            {
                Username = "akbarsafari00",
                Password = "Aa@123456"
            };
        });
        AllowAnonymous();
    }

    public override async Task HandleAsync(AuthLoginDto req, CancellationToken ct)
    {
        var authJwtDto = await _authService.LoginAsync(req, ct);
        await SendAsync(authJwtDto, cancellation: ct);
    }
}