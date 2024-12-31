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
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    }

    public override void Configure()
    {
        Post("/api/documents");
        AllowAnonymous(); // Adjust as per authentication requirements
    }

    public override async Task HandleAsync(CreateDocumentRequest req, CancellationToken ct)
    {
        var userId = GetCurrentUserId();

        if (userId == null)
        {
            ValidationFailures.Add(new("UserId", "User ID not found in claims."));
            await SendErrorsAsync(cancellation: ct);
            return;
        }

        var participantId = await GetParticipantIdByUserIdAsync(userId, ct);

        if (participantId == null)
        {
            ValidationFailures.Add(new("Participant", "Participant not found for the current user."));
            await SendErrorsAsync(cancellation: ct);
            return;
        }

        var receiverParticipantId = await GetReceiverParticipantIdAsync(req.ReceiverParticipantId, ct);

        if (receiverParticipantId == null)
        {
            ValidationFailures.Add(new("ReceiverParticipant", "Receiver participant not found."));
            await SendErrorsAsync(cancellation: ct);
            return;
        }

        var documentNumber = await GenerateDocumentNumberAsync(DateTime.UtcNow.Year, ct);

        var document = CreateDocument(req, participantId, receiverParticipantId, documentNumber);

        await _dbContext.Documents.AddAsync(document, ct);
        await _dbContext.SaveChangesAsync(ct);

        await SendOkAsync(DocumentDto.FromEntity(document), ct);
    }

    private async Task<ParticipantId?> GetParticipantIdByUserIdAsync(UserId userId, CancellationToken ct)
    {
        return await _dbContext.Participants
            .Where(p => p.UserId == userId)
            .Select(p => p.ParticipantId)
            .FirstOrDefaultAsync(ct);
    }

    private async Task<ParticipantId?> GetReceiverParticipantIdAsync(Guid receiverParticipantId, CancellationToken ct)
    {
        return await _dbContext.Participants
            .Where(p => p.ParticipantId == ParticipantId.NewId(receiverParticipantId))
            .Select(p => p.ParticipantId)
            .FirstOrDefaultAsync(ct);
    }

    private async Task<int> GenerateDocumentNumberAsync(int year, CancellationToken ct)
    {
        var count = await _dbContext.Documents
            .Where(d => d.LetterDate.Year == year)
            .CountAsync(ct);

        return count + 1;
    }

    private DocumentAggregateRoot CreateDocument(CreateDocumentRequest req, ParticipantId participantId, ParticipantId receiverParticipantId, int documentNumber)
    {
        return new DocumentAggregateRoot(
            req.Title,
            req.Content,
            participantId,
            receiverParticipantId,
            DocumentType.Internal,
            documentNumber
        );
    }

    private UserId? GetCurrentUserId()
    {
        var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
        return userIdClaim != null ? UserId.NewId(Guid.Parse(userIdClaim.Value)) : null;
    }
}
