using System.Security.Claims;
using FastEndpoints;
using FastEndpoints.Security;
using Nezam.EES.Service.Identity.Domains.Users.DomainServices;
using Nezam.EEs.Shared.Domain.Identity.User;
using Payeh.SharedKernel.UnitOfWork;

namespace Nezam.EES.Service.Identity.Application.UseCases.Authentication.Login;

public class AuthLoginEndpoint : Endpoint<AuthLoginRequest, AuthLoginResponse>
{
    private readonly IUserDomainService _userDomainService;
    private readonly IUnitOfWorkManager _workManager;
    private readonly IConfiguration _configuration;
    public AuthLoginEndpoint(IUserDomainService userDomainService, IUnitOfWorkManager workManager, IConfiguration configuration)
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
        var user = await _userDomainService.GetUserByUsernameAsync(UserNameId.NewId(req.UserName));

        if (user == null || !user.Data.Password.Validate(req.Password))
        {
            await SendUnauthorizedAsync(ct);
            return;
        }

        var claims = new[]
        {
            new Claim(ClaimTypes.Name, user.Data.UserName.Value),
            new Claim(ClaimTypes.NameIdentifier, user.Data.UserId.Value.ToString())

        }.ToList();
        
        claims.AddRange(user.Data.Roles.Select(x => new Claim(ClaimTypes.Role, x.RoleId.Value)));
        
        var token = JWTBearer.CreateToken(
            signingKey: _configuration["Jwt:SecretKey"],
            expireAt: DateTime.UtcNow.AddHours(1),
            claims: claims,
            roles:user.Data.Roles.Select(x=>x.RoleId.Value));

        await SendAsync(new AuthLoginResponse { AccessToken = token }, cancellation: ct);
        await uow.CommitAsync(ct);
    }
}
