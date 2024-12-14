using System.Security.Claims;
using FastEndpoints;
using FastEndpoints.Security;
using Nezam.Modular.ESS.Identity.Domain.User;
using Payeh.SharedKernel.EntityFrameworkCore.UnitOfWork;

namespace Nezam.Modular.ESS.WebApi.UseCases.Authentication;

public class AuthLoginRequest
{
    public string UserName { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}

public class AuthLoginResponse
{
    public string Token { get; set; } = string.Empty;
}

public class AuthLogin : Endpoint<AuthLoginRequest, AuthLoginResponse>
{
    private readonly IUserDomainService _userDomainService;
    private readonly IUnitOfWorkManager _workManager;
    private readonly IConfiguration _configuration;
    public AuthLogin(IUserDomainService userDomainService, IUnitOfWorkManager workManager, IConfiguration configuration)
    {
        _userDomainService = userDomainService;
        _workManager = workManager;
        _configuration = configuration;
    }

    public override void Configure()
    {
        Verbs(Http.POST);
        Routes("/api/auth/login");
        Summary(x => x.ExampleRequest = new AuthLoginRequest()
        {
            UserName = "admin",
            Password = "Admin@123456"
        });
        AllowAnonymous();
    }

    public override async Task HandleAsync(AuthLoginRequest req, CancellationToken ct)
    {
        using var uow = _workManager.Begin();
        var user = await _userDomainService.GetUserByUsernameAsync(new UserNameValue(req.UserName));

        if (user == null || !user.Data.Password.Validate(req.Password))
        {
            await SendUnauthorizedAsync(ct);
            return;
        }

        var token = JWTBearer.CreateToken(
            signingKey: _configuration["Jwt:SecretKey"],
            expireAt: DateTime.UtcNow.AddHours(1),
            claims: new[]
            {
                new Claim(ClaimTypes.Name, user.Data.UserName.Value),
                new Claim(ClaimTypes.NameIdentifier, user.Data.UserId.Value.ToString())
            });

        await SendAsync(new AuthLoginResponse { Token = token }, cancellation: ct);
        await uow.CommitAsync();
    }
}
