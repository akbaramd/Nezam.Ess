using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using Nezam.EES.Service.Identity.Application.Dto.Roles;
using Nezam.EES.Service.Identity.Infrastructure.EntityFrameworkCore;

namespace Nezam.EES.Service.Identity.Application.UseCases.Roles.GetRoles;

public class GetRolesEndpoint : Endpoint<GetRolesRequest, GetRolesResponse>
{
    private readonly IIdentityDbContext _dbContext;

    public GetRolesEndpoint(IIdentityDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public override void Configure()
    {
        Verbs(Http.GET);
        Routes("/api/roles");
        AllowAnonymous();
    }

    public override async Task HandleAsync(GetRolesRequest req, CancellationToken ct)
    {
        // Step 1: Prepare the query
        var query = _dbContext.Roles.AsNoTracking();

        // Step 2: Apply search filter if provided
        if (!string.IsNullOrWhiteSpace(req.Search))
        {
            string searchTerm = req.Search.Trim();
            query = query.Where(r => r.Title.Contains(searchTerm));
        }

        // Step 3: Get total count for pagination
        int totalCount = await query.CountAsync(ct);

        // Step 4: Fetch the data with pagination and map to DTO
        var roles = await query
            .Skip(req.Skip)
            .Take(req.Take)
            .ToListAsync(ct);

        // Map entities to DTOs using the FromEntity method
        var roleDtos = roles.Select(RoleDto.FromEntity).ToList();

        // Step 5: Return the response
        await SendAsync(new GetRolesResponse
        {
            Results = roleDtos,
            TotalCount = totalCount
        }, cancellation: ct);
    }
}