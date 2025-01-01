using System.Security.Claims;
using FastEndpoints;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Nezam.EEs.Shared.Domain.Identity.User;
using Nezam.EES.Slice.Secretariat.Application.Dto;
using Nezam.EES.Slice.Secretariat.Domains.Documents.ValueObjects;
using Nezam.EES.Slice.Secretariat.Infrastructure.EntityFrameworkCore;

namespace Nezam.EES.Slice.Secretariat.Application.UseCases.Documents.GetDocumentDetail
{
    public class GetDocumentDetailRequest
    {
        [FromRoute]
        public Guid DocumentId { get; set; }
    }
    public class GetDocumentEndpoint(ISecretariatDbContext dbContext) : Endpoint<GetDocumentDetailRequest, DocumentDto>
    {
        private readonly ISecretariatDbContext _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));

        public override void Configure()
        {
            Get("/api/documents/{documentId}");
        }

        public override async Task HandleAsync(GetDocumentDetailRequest req, CancellationToken ct)
        {
            var userId = GetCurrentUserId();

            if (userId == null)
            {
                ValidationFailures.Add(new("UserId", "User ID not found in claims."));
                await SendErrorsAsync(cancellation: ct);
                return;
            }

            var documentId = DocumentId.NewId(req.DocumentId);

            var document = await _dbContext.Documents
                .AsNoTracking()
                .Include(d => d.OwnerParticipant)
                .Include(d => d.ReceiverParticipant)
                .Include(d => d.Attachments)
                .Include(d => d.Referrals)
                .ThenInclude(r => r.ReceiverParticipant) // Ensure referral access verification
                .Include(d => d.Referrals)
                .ThenInclude(r => r.ReferrerParticipant) // Ensure referral access verification
                .Where(d => d.DocumentId == documentId)
                .FirstOrDefaultAsync(ct);

            if (document == null)
            {
                ValidationFailures.Add(new("Document", "Document not found."));
                await SendErrorsAsync(cancellation: ct);
                return;
            }

            var participantId = await _dbContext.Participants
                .Where(p => p.UserId == userId)
                .Select(p => p.ParticipantId)
                .FirstOrDefaultAsync(ct);

            if (participantId == null ||
                (document.OwnerParticipantId != participantId && document.ReceiverParticipantId != participantId && document.Referrals.All(r => r.ReceiverParticipantId != participantId)))
            {
                ValidationFailures.Add(new("Authorization", "Unauthorized access to the document or its referrals."));
                await SendUnauthorizedAsync(cancellation: ct);
                return;
            }

            var documentDto = DocumentDto.FromEntity(document);

            await SendOkAsync(documentDto, ct);
        }

        private UserId? GetCurrentUserId()
        {
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            return userIdClaim != null ? UserId.NewId(Guid.Parse(userIdClaim.Value)) : null;
        }
    }
}
