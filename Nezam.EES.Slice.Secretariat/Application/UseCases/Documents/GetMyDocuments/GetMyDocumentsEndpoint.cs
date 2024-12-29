using System.Security.Claims;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using Nezam.EEs.Shared.Domain.Identity.User;
using Nezam.EES.Slice.Secretariat.Application.Dto;
using Nezam.EES.Slice.Secretariat.Infrastructure.EntityFrameworkCore;

namespace Nezam.EES.Slice.Secretariat.Application.UseCases.Documents.GetMyDocuments;

public class GetMyDocumentsEndpoint : Endpoint<GetMyDocumentsRequest, GetMyDocumentsResponse>
{
    private readonly ISecretariatDbContext _dbContext;

    public GetMyDocumentsEndpoint(ISecretariatDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public override void Configure()
    {
        Get("/api/documents/mine");
    }

    public override async Task HandleAsync(GetMyDocumentsRequest req, CancellationToken ct)
    {
        // Extract current user ID from claims
        var userId = GetCurrentUserId();

        if (userId == null)
        {
            ThrowError("User ID not found in claims.");
        }

        // Find the participant ID associated with the user ID
        var participantId = await _dbContext.Participants
            .Where(p => p.UserId == userId)
            .Select(p => p.ParticipantId)
            .FirstOrDefaultAsync(ct);

        if (participantId == null)
        {
            ThrowError("Participant not found for the current user.");
        }

        // Query documents with pagination
        var query = _dbContext.Documents
            .AsNoTracking()
            .Include(d => d.OwnerParticipant)
            .Include(d => d.ReceiverParticipant)
            .Include(d => d.Referrals)
            .Where(d => d.OwnerParticipantId == participantId ||
                        d.ReceiverParticipantId == participantId ||
                        d.Referrals.Any(r => r.ReceiverUserId == participantId));

        var totalDocuments = await query.CountAsync(ct);

        var documents = await query
            .Skip(req.Skip)
            .Take(req.Take)
            .Select(c=>DocumentDto.FromEntity(c))
            .ToListAsync(ct);

        // Return paginated response
        await SendOkAsync(new GetMyDocumentsResponse()
        {
            TotalCount = totalDocuments,
            Results = documents,
        }, ct);
    }

    private UserId? GetCurrentUserId()
    {
        // Retrieve user ID from claims
        var userIdClaim =
            User.Claims.FirstOrDefault(c =>
                c.Type == ClaimTypes.NameIdentifier); // Adjust claim type as per your system
        return userIdClaim != null ? UserId.NewId(Guid.Parse(userIdClaim.Value)) : null;
    }
}