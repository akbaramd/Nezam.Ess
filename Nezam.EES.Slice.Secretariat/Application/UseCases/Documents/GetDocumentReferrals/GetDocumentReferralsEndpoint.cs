using System.Security.Claims;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using Nezam.EEs.Shared.Domain.Identity.User;
using Nezam.EES.Slice.Secretariat.Application.Dto;
using Nezam.EES.Slice.Secretariat.Domains.Documents.ValueObjects;
using Nezam.EES.Slice.Secretariat.Infrastructure.EntityFrameworkCore;

namespace Nezam.EES.Slice.Secretariat.Application.UseCases.Documents.GetDocumentReferrals
{
    public class GetDocumentReferralsRequest
    {
        public Guid DocumentId { get; set; }
    }

    public class GetDocumentReferralsResponse
    {
        public List<DocumentReferralDto> Referrals { get; set; }
    }


    public class GetDocumentReferralsEndpoint : Endpoint<GetDocumentReferralsRequest, GetDocumentReferralsResponse>
    {
        private readonly ISecretariatDbContext _dbContext;

        public GetDocumentReferralsEndpoint(ISecretariatDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public override void Configure()
        {
            Get("/api/documents/{documentId}/referrals");
            // Note: The {documentId} part should map to the DocumentId parameter.
        }

        public override async Task HandleAsync(GetDocumentReferralsRequest req, CancellationToken ct)
        {
            // Extract current user ID from claims
            var userId = GetCurrentUserId();

            if (userId == null)
            {
                ThrowError("User ID not found in claims.");
            }
            var documentId = DocumentId.NewId(req.DocumentId);
            // Query the document to ensure the user has access
            var document = await _dbContext.Documents
                .AsNoTracking()
                .Include(d => d.Referrals).ThenInclude(d => d.ReferrerUser)
                .Include(d => d.Referrals).ThenInclude(d => d.ReceiverUser)
                .Where(d => d.DocumentId == documentId)
                .FirstOrDefaultAsync(ct);

            if (document == null)
            {
                ThrowError("Document not found.");
            }

            // Ensure user is authorized to access this document (customize this check)
            var participantId = await _dbContext.Participants
                .Where(p => p.UserId == userId)
                .Select(p => p.ParticipantId)
                .FirstOrDefaultAsync(ct);

            if (participantId == null || 
                (document.OwnerParticipantId != participantId && document.ReceiverParticipantId != participantId))
            {
                ThrowError("Unauthorized access to the document.");
            }

            // Map to the response DTO
            var referrals = document.Referrals.Select(DocumentReferralDto.FromEntity).ToList();

            // Send the response with document referrals
            await SendOkAsync(new GetDocumentReferralsResponse { Referrals = referrals }, ct);
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
}
