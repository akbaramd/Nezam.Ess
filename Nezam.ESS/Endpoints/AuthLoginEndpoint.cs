using FastEndpoints;
using FastEndpoints.Security;
using Microsoft.EntityFrameworkCore;
using Nezam.ESS.backend.Data;

namespace Nezam.ESS.backend.Endpoints;

public class AuthLoginEndpointRequest
{
    public string UserName { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}

public class AuthLoginEndpointResponse
{
    public string AccessToken { get; set; } = string.Empty;
    public DateTime ExpireAt { get; set; }
    public string? LastName { get; set; }
    public string? FirstName { get; set; }
    public string UserName { get; set; } = string.Empty;
}

public class AuthLoginEndpoint : Endpoint<AuthLoginEndpointRequest, AuthLoginEndpointResponse>
{
    private readonly AppDbContext _dbContext;

    public AuthLoginEndpoint(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public override void Configure()
    {
        Post("/api/auth/login");
        AllowAnonymous();
        Summary(c =>
        {
            c.ExampleRequest = new AuthLoginEndpointRequest
            {
                UserName = "143001982",
                Password = "13580430"
            };
        });

    }

    public override async Task HandleAsync(AuthLoginEndpointRequest req, CancellationToken ct)
    {
        var user = await _dbContext.TblEngineers.FirstOrDefaultAsync(x =>
            x.Password != null && x.OzviyatNo.ToString().Equals(req.UserName) &&
            x.Password.ToString().Equals(req.Password));

        if (user is null)
        {
            await SendUnauthorizedAsync(ct);
            return;
        }


        var jwtToken = JwtBearer.CreateToken(
            o =>
            {
                o.SigningKey = "Neza23423423m423324ES233SS234yst3424e33242234mI42d";
                o.ExpireAt = DateTime.UtcNow.AddDays(1);
                o.User.Roles.Add("User");
                o.User.Claims.Add(("Id", user.OzviyatNo.ToString()));
                o.User.Claims.Add(("UserName", req.UserName));
                o.User.Claims.Add(("RegistrationNumber", user.OzviyatNo.ToString()));
                o.User.Claims.Add(("Agency", user.DaftarNo ?? "0"));
            });

        await SendAsync(
            new AuthLoginEndpointResponse
            {
                AccessToken = jwtToken,
                ExpireAt = DateTime.UtcNow.AddDays(1),
                UserName = req.UserName,
                FirstName = user.Name,
                LastName = user.Fname
            });
    }
}