using FastEndpoints;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Nezam.Modular.ESS.Identity.Application.Roles;
using Nezam.Modular.ESS.Infrastructure.Data;

namespace Nezam.Modular.ESS.WebApi.UseCases.Roles;

public class GetRolesRequest
{
    [FromQuery]
    public int Take { get; set; } = 10; // Default page size

    [FromQuery]
    public int Skip { get; set; } = 0; // Default page index

    [FromQuery]
    public string? Search { get; set; } // Optional search term
}

public class GetRolesResponse
{
    public List<RoleDto> Roles { get; set; } = new(); // DTO for security
    public int TotalCount { get; set; }
}


public class GetRolesEndpoint : Endpoint<GetRolesRequest, GetRolesResponse>
{
    private readonly AppDbContext _dbContext;

    public GetRolesEndpoint(AppDbContext dbContext)
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
            Roles = roleDtos,
            TotalCount = totalCount
        }, cancellation: ct);
    }
}