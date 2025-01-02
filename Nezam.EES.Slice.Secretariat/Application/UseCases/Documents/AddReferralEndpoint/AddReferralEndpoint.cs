using System.Security.Claims;
using FastEndpoints;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Nezam.EEs.Shared.Domain.Identity.User;
using Nezam.EES.Slice.Secretariat.Domains.Documents;
using Nezam.EES.Slice.Secretariat.Domains.Documents.Repositories;
using Nezam.EES.Slice.Secretariat.Domains.Documents.ValueObjects;
using Nezam.EES.Slice.Secretariat.Domains.Participant;
using Nezam.EES.Slice.Secretariat.Domains.Participant.Repositories;
using Payeh.SharedKernel.UnitOfWork;

namespace Nezam.EES.Slice.Secretariat.Application.UseCases.Documents.AddReferralEndpoint;

public class AddReferralEndpoint : Endpoint<AddReferralRequest>
{
    private readonly IDocumentRepository _documentRepository;
    private readonly IParticipantRepository _participantRepository;
    private readonly IUnitOfWorkManager _unitOfWorkManager;

    public AddReferralEndpoint(IDocumentRepository documentRepository, IUnitOfWorkManager unitOfWorkManager, IParticipantRepository participantRepository)
    {
        _documentRepository = documentRepository;
        _unitOfWorkManager = unitOfWorkManager;
        _participantRepository = participantRepository;
    }

    public override void Configure()
    {
        Post("/api/documents/{DocumentId}/add-referral");
    }

    public override async Task HandleAsync(AddReferralRequest req, CancellationToken ct)
    {
        using var uow = _unitOfWorkManager.Begin();

        // Get the current user's ParticipantId from the context
        var userID = GetCurrentUserId();
        if (userID == null)
        {
            AddError("Access", "User is not authorized or ParticipantId is missing.");
            await SendErrorsAsync(cancellation: ct);
            return;
        }
        var referrerParticipantId = await GetParticipantIdByUserIdAsync(userID, ct);
        // Validate Document ID from route
        var documentId = DocumentId.NewId(req.DocumentId);

        // Fetch the document
        var document = await _documentRepository.FindOneAsync(x => x.DocumentId == documentId);
        if (document == null)
        {
            AddError("Document", "Document not found.");
            await SendErrorsAsync(cancellation: ct);
            return;
        }

        // Check access control: Ensure ReferrerParticipantId has access to this document
        if (!HasAccessToDocument(referrerParticipantId.Value, document))
        {
            AddError("Access", "You do not have access to this document.");
            await SendErrorsAsync(cancellation: ct);
            return;
        }

        // Validate participants
        if (referrerParticipantId.Value == req.ReceiverParticipantId)
        {
            AddError("Participants", "You cannot refer a document to yourself.");
            await SendErrorsAsync(cancellation: ct);
            return;
        }

        // Validate content
        if (string.IsNullOrWhiteSpace(req.Content))
        {
            AddError("Content", "Content cannot be empty.");
            await SendErrorsAsync(cancellation: ct);
            return;
        }

        // Handle Parent Referral
        if (req.ParentReferralId.HasValue)
        {
            var parentReferralId = DocumentReferralId.NewId(req.ParentReferralId.Value);
            var parentReferral = document.Referrals.FirstOrDefault(r => r.DocumentReferralId == parentReferralId);

            if (parentReferral == null)
            {
                AddError("ParentReferral", "Parent referral not found.");
                await SendErrorsAsync(cancellation: ct);
                return;
            }

            if (parentReferral.ReceiverParticipantId.Value != referrerParticipantId.Value)
            {
                AddError("Access", "You do not have permission to respond to this referral.");
                await SendErrorsAsync(cancellation: ct);
                return;
            }

            // Respond to the parent referral
            parentReferral.Respond();
        }

        // Add a new referral regardless of whether ParentReferralId exists
        document.AddReferral(
            ParticipantId.NewId(req.ReceiverParticipantId),
            ParticipantId.NewId(referrerParticipantId.Value),
            req.Content,
            req.ParentReferralId.HasValue ? DocumentReferralId.NewId(req.ParentReferralId.Value) : null
        );

        // Save changes
        await _documentRepository.UpdateAsync(document, true);
        await uow.CommitAsync(ct);

        await SendOkAsync(new { Message = "Referral added successfully." }, ct);
    }


    private async Task<ParticipantId?> GetParticipantIdByUserIdAsync(UserId userId, CancellationToken ct)
    {
        return (await _participantRepository.GetOneAsync(x=>x.UserId == userId)).ParticipantId;
    }
    private bool HasAccessToDocument(Guid referrerParticipantId, DocumentAggregateRoot document)
    {
        // Check if the referrer is either a referrer or receiver in the document's existing referrals
        return document.Referrals.Any(r =>
            r.ReferrerParticipantId.Value == referrerParticipantId ||
            r.ReceiverParticipantId.Value == referrerParticipantId);
    }
    
    private UserId? GetCurrentUserId()
    {
        var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
        return userIdClaim != null ? UserId.NewId(Guid.Parse(userIdClaim.Value)) : null;
    }
}

public class AddReferralRequest
{
    [FromRoute]
    public Guid DocumentId { get; set; }

    public Guid ReceiverParticipantId { get; set; }

    public string Content { get; set; } = default!; // Content for the referral

    public Guid? ParentReferralId { get; set; }
}

public class AddReferralRequestValidator : Validator<AddReferralRequest>
{
    public AddReferralRequestValidator()
    {
        RuleFor(x => x.DocumentId).NotEmpty().WithMessage("Document ID is required.");
        RuleFor(x => x.ReceiverParticipantId).NotEmpty().WithMessage("Receiver participant ID is required.");
        RuleFor(x => x.Content).NotEmpty().WithMessage("Content is required.");
    }
}
