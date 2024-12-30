using Nezam.EES.Slice.Secretariat.Domains.Documents.Enumerations;
using Nezam.EES.Slice.Secretariat.Domains.Documents.ValueObjects;
using Nezam.EES.Slice.Secretariat.Domains.Participant;
using Payeh.SharedKernel.Domain;

namespace Nezam.EES.Slice.Secretariat.Domains.Documents;

public class DocumentReferralEntity : Entity
{
    public DocumentReferralId DocumentReferralId { get; private set; }
    public DocumentId DocumentId { get; private set; }
    public ParticipantId ReferrerUserId { get; private set; } // Sender of the referral
    public Participant.Participant ReferrerUser { get; private set; }
    public ParticipantId ReceiverUserId { get; private set; } // Receiver of the referral
    public Participant.Participant ReceiverUser { get; private set; }
    public ReferralStatus Status { get; private set; }
    public DateTime ReferralDate { get; private set; }
    public DateTime? ViewedDate { get; private set; }
    public DateTime? RespondedDate { get; private set; }
    public string? ResponseContent { get; private set; }
    public DocumentReferralId? ParentReferralId { get; private set; } // Parent referral for hierarchy management

    // Constructor with validation
    public DocumentReferralEntity(
        DocumentId documentId,
        ParticipantId referrerUserId,
        ParticipantId receiverUserId,
        DocumentReferralId? parentReferralId = null)
    {
        DocumentReferralId = DocumentReferralId.NewId();
        DocumentId = documentId ?? throw new ArgumentNullException(nameof(documentId));
        ReferrerUserId = referrerUserId ?? throw new ArgumentNullException(nameof(referrerUserId));
        ReceiverUserId = receiverUserId ?? throw new ArgumentNullException(nameof(receiverUserId));
        ParentReferralId = parentReferralId;
        Status = ReferralStatus.Pending; // Default status
        ReferralDate = DateTime.UtcNow;
    }

    /// <summary>
    /// Marks the referral as viewed.
    /// </summary>
    public void MarkAsViewed()
    {
        if (Status == ReferralStatus.Canceled)
            throw new InvalidOperationException("Cannot mark a canceled referral as viewed.");

        if (Status == ReferralStatus.Responded)
            throw new InvalidOperationException("Cannot mark a responded referral as viewed.");

        Status = ReferralStatus.Viewed;
        ViewedDate = DateTime.UtcNow;
    }

    /// <summary>
    /// Responds to the referral.
    /// </summary>
    /// <param name="responseContent">The response content.</param>
    public void Respond(string responseContent)
    {
        if (Status != ReferralStatus.Pending)
            throw new InvalidOperationException("Only pending referrals can be responded to.");

        if (string.IsNullOrWhiteSpace(responseContent))
            throw new ArgumentException("Response content cannot be empty.", nameof(responseContent));

        Status = ReferralStatus.Responded;
        RespondedDate = DateTime.UtcNow;
        ResponseContent = responseContent;
    }

    /// <summary>
    /// Cancels the referral.
    /// </summary>
    public void Cancel()
    {
        if (Status == ReferralStatus.Responded)
            throw new InvalidOperationException("Cannot cancel a referral that has already been responded to.");

        if (Status == ReferralStatus.Canceled)
            throw new InvalidOperationException("Referral is already canceled.");

        Status = ReferralStatus.Canceled;
    }

    /// <summary>
    /// Checks if the referral has been processed (responded).
    /// </summary>
    public bool IsProcessed()
    {
        return Status == ReferralStatus.Responded;
    }

    public override object GetKey()
    {
        return DocumentReferralId;
    }
}