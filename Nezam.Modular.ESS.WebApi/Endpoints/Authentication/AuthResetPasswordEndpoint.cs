using Bonyan.ExceptionHandling;
using FastEndpoints;
using Nezam.Modular.ESS.Identity.Application.Auth;
using Nezam.Modular.ESS.Identity.Application.Auth.Dto;

namespace Nezam.Modular.ESS.WebApi.Endpoints.Authentication;

public class AuthResetPasswordEndpoint : Endpoint<AuthResetPasswordDto>
{
    private readonly IAuthService _authService;

    public AuthResetPasswordEndpoint(IAuthService authService)
    {
        _authService = authService;
    }

    public override void Configure()
    {
        Post("/api/auth/reset-password");
      
        Description(c =>
        {
            c.Produces<HttpExceptionModel>(500);
        });
        Summary(c =>
        {
            c.ExampleRequest = new AuthResetPasswordDto()
            {
               Password  = "Aa@123456789",
               ConfirmPassword  = "Aa@123456789",
                VerificationToken = "xxxxxxxxxxxxxxxxxxxxxxx"
            };
        });
        AllowAnonymous();
    }

    public override async Task HandleAsync(AuthResetPasswordDto req, CancellationToken ct)
    {
         await _authService.ResetPasswordAsync(req, ct);
        await SendOkAsync(ct);
    }
}