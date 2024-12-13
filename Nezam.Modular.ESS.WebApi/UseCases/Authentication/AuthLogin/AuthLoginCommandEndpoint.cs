using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using FastEndpoints;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Nezam.Modular.ESS.Identity.Domain.Shared.User;
using Nezam.Modular.ESS.Identity.Domain.User;
using Payeh.SharedKernel.EntityFrameworkCore.UnitOfWork;

namespace Nezam.Modular.ESS.WebApi.Endpoints.Authentication.AuthLogin;

public class AuthLoginRequest
{
    public string UserName { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}

public class AuthLoginResponse
{
    public string Token { get; set; } = string.Empty;
}

public class AuthLoginCommandEndpoint : Endpoint<AuthLoginRequest, AuthLoginResponse>
{
    private readonly IUserDomainService _userDomainService;
    private readonly IUnitOfWorkManager _workManager;
    private readonly IConfiguration _configuration;

    public AuthLoginCommandEndpoint(IUserDomainService userDomainService, IConfiguration configuration, IUnitOfWorkManager workManager)
    {
        _userDomainService = userDomainService;
        _configuration = configuration;
        _workManager = workManager;
    }

    public override void Configure()
    {
        Verbs(Http.POST);
        Routes("/api/auth/login");
        AllowAnonymous();
    }

    public override async Task HandleAsync(AuthLoginRequest req, CancellationToken ct)

    {
        using var uow = _workManager.Begin();
        var user = await _userDomainService.GetUserByUsernameAsync(new UserNameValue(req.UserName));

        if (user == null || !user.Data.Password.Validate( req.Password))
        {
            await SendUnauthorizedAsync(ct);
            return;
        }

        var token = GenerateJwtToken(user.Data);
        await SendAsync(new AuthLoginResponse { Token = token }, cancellation: ct);
        await uow.CommitAsync();
    }

    private string GenerateJwtToken(UserEntity user)
    {
        var key = Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]);
        var issuer = _configuration["Jwt:Issuer"];
        var audience = _configuration["Jwt:Audience"];

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.UserName.Value),
            new Claim(ClaimTypes.NameIdentifier, user.UserId.Value.ToString())
        };

        var credentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256);

        var tokenDescriptor = new JwtSecurityToken(
            issuer: issuer,
            audience: audience,
            claims: claims,
            expires: DateTime.UtcNow.AddHours(1),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
    }
}
