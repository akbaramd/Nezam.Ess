using System.Security.Claims;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using Nezam.EES.Service.Identity.Application.Dto.Users;
using Nezam.EES.Service.Identity.Infrastructure.EntityFrameworkCore;
using Nezam.EEs.Shared.Domain.Identity.User;
using Payeh.SharedKernel.UnitOfWork;

namespace Nezam.EES.Service.Identity.Application.UseCases.Authentication.GetProfile;


public class AutGetProfileEndpoint : EndpointWithoutRequest<UserDto>
{
    private readonly IIdentityDbContext _dbContext;
    public AutGetProfileEndpoint(IIdentityDbContext dbContext, IUnitOfWorkManager workManager)
    {
        _dbContext = dbContext;
    }

    public override void Configure()
    {
        Verbs(Http.GET);
        Routes("/api/auth/profile");
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        // Extract userId from JWT
        var userIdClaim = HttpContext.User?.FindFirst(ClaimTypes.NameIdentifier);
        if (userIdClaim == null || !Guid.TryParse(userIdClaim.Value, out Guid userId))
        {
            AddError("User is not authenticated.");
            await SendErrorsAsync(cancellation: ct);
            return;
        }

        // Start Unit of Work

        // Retrieve user from DbContext by ID, using AsNoTracking for better performance
        var user = await _dbContext.Users
            .AsNoTracking()
            .Include(x=>x.Roles)
            .Include(x=>x.Departments)
            .Where(u => u.UserId == UserId.NewId(userId))
            .FirstOrDefaultAsync(ct);

        if (user == null)
        {
            AddError("User not found.");
            await SendErrorsAsync(cancellation: ct);
            return;
        }
        // Send successful response
        await SendAsync(UserDto.FromEntity(user), cancellation: ct);
    }
}
