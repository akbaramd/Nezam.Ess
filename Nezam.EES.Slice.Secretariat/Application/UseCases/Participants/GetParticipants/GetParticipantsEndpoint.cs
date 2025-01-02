using System.Linq.Dynamic.Core;
using System.Security.Claims;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using Nezam.EES.Slice.Secretariat.Application.Dto;
using Nezam.EES.Slice.Secretariat.Infrastructure.EntityFrameworkCore;
using Nezam.EES.Slice.Secretariat.Domains.Participant;

namespace Nezam.EES.Slice.Secretariat.Application.UseCases.Participants.GetParticipants;

public class GetParticipantsEndpoint : Endpoint<GetParticipantsRequest, GetParticipantsResponse>
{
    private readonly ISecretariatDbContext _dbContext;

    public GetParticipantsEndpoint(ISecretariatDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public override void Configure()
    {
        Verbs(Http.GET);
        Routes("/api/participants");
        AllowAnonymous(); // Adjust based on your authentication requirements
    }

  public override async Task HandleAsync(GetParticipantsRequest req, CancellationToken ct)
{
    // Step 1: Prepare the query
    var query = _dbContext.Participants.AsNoTracking();
    

    // Step 2: Apply role-based filtering based on all roles
    if (req.Roles.Any())
    {
        query = req.Roles.Aggregate(query, (currentQuery, role) =>
        {
            return role.ToLower() switch
            {
                "engineer" => currentQuery.Where(x=>x.ParticipantType == ParticipantType.Department),
                "admin" => currentQuery, // Admins can see all participants
                "employer" => currentQuery, // Employers can see all participants
                _ => currentQuery // Unknown roles: No additional filtering
            };
        });
    }

    // Step 3: Apply dynamic filtering based on Fields
    if (!string.IsNullOrWhiteSpace(req.Filters))
    {
        var filters = req.Filters.Split(',')
                                .Select(f => f.Split(':'))
                                .Where(f => f.Length == 2)
                                .ToDictionary(f => f[0].Trim(), f => f[1].Trim());

        foreach (var filter in filters)
        {
            query = filter.Key switch
            {
                "Name" => query.Where(p => p.Name.Contains(filter.Value)),
                "Authority" => query.Where(p => p.Authority.Contains(filter.Value)),
                _ => query // Ignore invalid filters
            };
        }
    }

    // Step 4: Apply dynamic sorting based on Sorts
    if (!string.IsNullOrWhiteSpace(req.Sorts))
    {
        var sortFields = req.Sorts.Split(',')
                                  .Select(s => s.Split(':'))
                                  .Where(s => s.Length == 2)
                                  .Select(s => new { Field = s[0].Trim(), Direction = s[1].Trim().ToLower() });

        foreach (var sortField in sortFields)
        {
            query = sortField.Field switch
            {
                "Name" => sortField.Direction == "desc" ? query.OrderByDescending(p => p.Name) : query.OrderBy(p => p.Name),
                "Authority" => sortField.Direction == "desc" ? query.OrderByDescending(p => p.Authority) : query.OrderBy(p => p.Authority),
                _ => query // Ignore invalid sort fields
            };
        }
    }
    else
    {
        // Default sorting by Name if no sort order provided
        query = query.OrderBy(p => p.Name);
    }

    // Step 5: Get total count for pagination
    int totalCount = await query.CountAsync(ct);

    // Step 6: Fetch the data with pagination
    var participants = await query
        .Skip(req.Skip)
        .Take(req.Take)
        .ToListAsync(ct);

    // Step 7: Map to DTOs
    var participantDtos = participants.Select(ParticipantDto.FromEntity).ToList();

    // Step 8: Return the response
    await SendAsync(new GetParticipantsResponse
    {
        Results = participantDtos,
        TotalCount = totalCount
    }, cancellation: ct);
}

}
