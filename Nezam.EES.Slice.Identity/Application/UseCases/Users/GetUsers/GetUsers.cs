using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using Nezam.EES.Service.Identity.Application.Dto.Users;
using Nezam.EES.Service.Identity.Infrastructure.EntityFrameworkCore;
using System.Linq.Dynamic.Core;
using Nezam.EEs.Shared.Domain.Identity.User;
using Nezam.EEs.Shared.Domain.Identity.User.ValueObjects;

namespace Nezam.EES.Service.Identity.Application.UseCases.Users.GetUsers;

public class GetUsersEndpoint : Endpoint<GetUsersRequest, GetUsersResponse>
{
    private readonly IIdentityDbContext _dbContext;

    public GetUsersEndpoint(IIdentityDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public override void Configure()
    {
        Verbs(Http.GET);
        Routes("/api/users");
        AllowAnonymous();
    }

   public override async Task HandleAsync(GetUsersRequest req, CancellationToken ct)
{
    // Step 1: Prepare the query
    var query = _dbContext.Users.Include(x => x.Roles).AsNoTracking();

    // Step 2: Apply filters
    if (req.Filters != null && req.Filters.Any())
    {
        var sections= req.Filters.Split(',');
        foreach (var filter in sections)
        {
            var parts = filter.Split(':');
            if (parts.Length == 2)
            {
                var propertyPath = parts[0];
                var value = parts[1];

                switch (propertyPath)
                {
                    case "UserName":
                        query = query.Where(c => c.UserName == UserNameId.NewId(value));
                        break;
                    case "Email":
                        query = query.Where(c => c.Email != null && c.Email.Value == value);
                        break;
                    case "FirstName":
                        query = query.Where(c => c.Profile != null && c.Profile.FirstName.Contains(value));
                        break;
                    case "LastName":
                        query = query.Where(c => c.Profile != null && c.Profile.LastName.Contains(value));
                        break;
                }
            }
        }
    }

    // Step 3: Apply sorting
    if (!string.IsNullOrWhiteSpace(req.Sorting))
    {
        var sortingParts = req.Sorting.Split(':');
        if (sortingParts.Length == 2)
        {
            var propertyPath = sortingParts[0];
            var direction = sortingParts[1].ToLower();

            switch (propertyPath)
            {
                case "UserName":
                    query = direction == "desc"
                        ? query.OrderByDescending(u => u.UserName.Value)
                        : query.OrderBy(u => u.UserName.Value);
                    break;
                case "Email":
                    query = direction == "desc"
                        ? query.OrderByDescending(u => u.Email.Value)
                        : query.OrderBy(u => u.Email.Value);
                    break;
                case "FirstName":
                    query = direction == "desc"
                        ? query.OrderByDescending(u => u.Profile.FirstName)
                        : query.OrderBy(u => u.Profile.FirstName);
                    break;
                case "LastName":
                    query = direction == "desc"
                        ? query.OrderByDescending(u => u.Profile.LastName)
                        : query.OrderBy(u => u.Profile.LastName);
                    break;
                default:
                    throw new ArgumentException($"Unsupported sorting property: {propertyPath}");
            }
        }
    }
    else
    {
        // Default sorting
        query = query.OrderBy(u => u.Email.Value);
    }

    // Step 4: Get total count for pagination
    int totalCount = await query.CountAsync(ct);

    // Step 5: Fetch the data with pagination and map to DTO
    var users = await query
        .Skip(req.Skip)
        .Take(req.Take)
        .ToListAsync(ct);

    var userDtos = users.Select(UserDto.FromEntity).ToList();

    // Step 6: Return the response
    await SendAsync(new GetUsersResponse
    {
        Results = userDtos,
        TotalCount = totalCount
    }, cancellation: ct);
}


}
