using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using Nezam.EES.Slice.Secretariat.Application.Dto;
using Nezam.EES.Slice.Secretariat.Infrastructure.EntityFrameworkCore;

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

        // Step 2: Apply search filter if provided
        if (!string.IsNullOrWhiteSpace(req.Search))
        {
            string searchTerm = req.Search.Trim();
            query = query.Where(p => 
                p.Name.Contains(searchTerm));
        }

        // Step 3: Get total count for pagination
        int totalCount = await query.CountAsync(ct);

        // Step 4: Fetch the data with pagination and map to DTO
        var participants = await query
            .Skip(req.Skip)
            .Take(req.Take)
            .ToListAsync(ct);

        var participantDtos = participants.Select(ParticipantDto.FromEntity).ToList();

        // Step 5: Return the response
        await SendAsync(new GetParticipantsResponse
        {
            Results = participantDtos,
            TotalCount = totalCount
        }, cancellation: ct);
    }
}