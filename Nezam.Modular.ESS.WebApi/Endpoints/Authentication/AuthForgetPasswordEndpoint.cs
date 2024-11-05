using Bonyan.ExceptionHandling;
using FastEndpoints;
using Nezam.Modular.ESS.Identity.Application.Auth;
using Nezam.Modular.ESS.Identity.Application.Auth.Dto;

namespace Nezam.Modular.ESS.WebApi.Endpoints.Authentication;

public class AuthForgetPasswordEndpoint : Endpoint<AuhForgetPasswordDto,AuhForgetPasswordResult>
{
    private readonly IAuthService _authService;

    public AuthForgetPasswordEndpoint(IAuthService authService)
    {
        _authService = authService;
    }

    public override void Configure()
    {
        Post("/api/auth/forget-password");

        Description(c =>
        {
            c.Produces<AuhForgetPasswordResult>(200);
            c.Produces<HttpExceptionModel>(500);
            c.WithTags("Authentication");
        });
        Summary(c =>
        {
            c.ExampleRequest = new AuhForgetPasswordDto()
            {
               Username  = "akbarsafari00",
         
            };
        });
        AllowAnonymous();
    }

    public override async Task HandleAsync(AuhForgetPasswordDto req, CancellationToken ct)
    {
        var authJwtDto = await _authService.ForgetPasswordAsync(req, ct);
        await SendAsync(authJwtDto, cancellation: ct);
    }
}