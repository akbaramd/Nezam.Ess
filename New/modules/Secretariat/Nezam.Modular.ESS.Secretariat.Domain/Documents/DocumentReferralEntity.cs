using Bonyan.Layer.Domain.Entities;
using Bonyan.UserManagement.Domain.ValueObjects;


namespace Nezam.Modular.ESS.Secretariat.Domain.Documents;

public class DocumentReferralEntity : Entity<DocumentReferralId>
{
    public DocumentId DocumentId { get; private set; }
    public UserId ReferrerUserId { get; private set; } // The user who created this referral
    public UserId ReceiverUserId { get; private set; }
    public ReferralStatus Status { get; private set; }
    public DateTime ReferralDate { get; private set; }
    public DateTime? ViewedDate { get; private set; }
    public DateTime? RespondedDate { get; private set; }
    public string ResponseContent { get; private set; }
    public DocumentReferralId? NextReferralId { get; private set; } // Link to the next referral in the pipeline

    // Updated Constructor with ReferrerUserId
    public DocumentReferralEntity(DocumentId documentId, UserId referrerUserId, UserId receiverUserId)
    {
        DocumentId = documentId;
        ReferrerUserId = referrerUserId;
        ReceiverUserId = receiverUserId;
        Status = ReferralStatus.New;
        ReferralDate = DateTime.UtcNow;
    }

    // Behavior to mark referral as viewed by receiver
    public void MarkAsViewed()
    {
        if (Status == ReferralStatus.Responded)
            throw new InvalidOperationException("Cannot mark a responded referral as viewed.");

        Status = ReferralStatus.Viewed;
        ViewedDate = DateTime.UtcNow;
    }

    // Behavior to respond to the referral
    public void Respond(string responseContent)
    {
        if (Status == ReferralStatus.Responded)
            throw new InvalidOperationException("Referral has already been responded to.");

        Status = ReferralStatus.Responded;
        RespondedDate = DateTime.UtcNow;
        ResponseContent = responseContent;
    }

    public void Cancel()
    {
        Status = ReferralStatus.Canceled;
    }
    
    // Behavior to set the next referral in the pipeline
    public void SetNextReferral(DocumentReferralId nextReferralId)
    {
        NextReferralId = nextReferralId;
    }

    // Check if this referral has been fully processed (responded)
    public bool IsProcessed()
    {
        return Status == ReferralStatus.Responded;
    }
}
