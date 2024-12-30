using System.Security.Claims;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using Nezam.EEs.Shared.Domain.Identity.User;
using Nezam.EES.Slice.Secretariat.Application.Dto;
using Nezam.EES.Slice.Secretariat.Domains.Documents;
using Nezam.EES.Slice.Secretariat.Domains.Documents.Enumerations;
using Nezam.EES.Slice.Secretariat.Domains.Participant;
using Nezam.EES.Slice.Secretariat.Infrastructure.EntityFrameworkCore;

namespace Nezam.EES.Slice.Secretariat.Application.UseCases.Documents.CreateDocument;

public class CreateDocumentEndpoint : Endpoint<CreateDocumentRequest>
{
    private readonly ISecretariatDbContext _dbContext;

    public CreateDocumentEndpoint(ISecretariatDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public override void Configure()
    {
        Post("/api/documents");
    }

    public override async Task HandleAsync(CreateDocumentRequest req, CancellationToken ct)
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

        // Find the participant ID associated with the receiver ID
        var participantReceiverId = await _dbContext.Participants
            .Where(p => p.ParticipantId == ParticipantId.NewId(req.ReceiverParticipantId))
            .Select(p => p.ParticipantId)
            .FirstOrDefaultAsync(ct);

        if (participantReceiverId == null)
        {
            ThrowError("Receiver participant not found.");
        }

        // Generate a unique document number for the current year
        var currentYear = DateTime.UtcNow.Year;
        var documentNumber = await GenerateDocumentNumberAsync(currentYear, ct);

        // Create the document with the generated number
        var document = new DocumentAggregateRoot(
            req.Title,
            req.Content,
            participantId,
            participantReceiverId,
            DocumentType.Internal,
            documentNumber
        );

        // Save the document
        await _dbContext.Documents.AddAsync(document, ct);
        await _dbContext.SaveChangesAsync(ct);

        // Return response
        await SendOkAsync(DocumentDto.FromEntity(document), ct);
    }

    private async Task<int> GenerateDocumentNumberAsync(int year, CancellationToken ct)
    {
        // Count documents created in the current year
        var count = await _dbContext.Documents
            .Where(d => d.LetterDate.Year == year)
            .CountAsync(ct);

        // Increment by 1 to generate the new document number
        return count + 1;
    }

    private UserId? GetCurrentUserId()
    {
        // Retrieve user ID from claims
        var userIdClaim = User.Claims.FirstOrDefault(c =>
            c.Type == ClaimTypes.NameIdentifier); // Adjust claim type as per your system
        return userIdClaim != null ? UserId.NewId(Guid.Parse(userIdClaim.Value)) : null;
    }
}
