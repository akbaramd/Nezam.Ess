using System.Text;
using Nezam.EEs.Shared.Domain.Identity.User;
using Nezam.EES.Slice.Secretariat.Domains.Documents.Enumerations;
using Nezam.EES.Slice.Secretariat.Domains.Documents.Events;
using Nezam.EES.Slice.Secretariat.Domains.Documents.ValueObjects;
using Nezam.EES.Slice.Secretariat.Domains.Participant;
using Payeh.SharedKernel.Domain;
using Payeh.SharedKernel.Exceptions;

namespace Nezam.EES.Slice.Secretariat.Domains.Documents;

public class DocumentAggregateRoot : AggregateRoot
{
    public DocumentId DocumentId { get; set; }
    public string Title { get; private set; }
    public string Content { get; private set; }
    public ParticipantId OwnerParticipantId { get; private set; }
    public Participant.Participant OwnerParticipant { get; private set; }
    public ParticipantId ReceiverParticipantId { get; private set; }
    public Participant.Participant ReceiverParticipant { get; private set; }
    public DocumentType Type { get; private set; }
    public DocumentStatus Status { get; private set; }
    public string TrackingCode { get; private set; } // Unique tracking code
    public int LetterNumber { get; private set; } // Sequential number, can repeat annually
    public DateTime LetterDate { get; private set; } // Current date when document is created

    private readonly List<DocumentAttachmentEntity> _attachments = new List<DocumentAttachmentEntity>();
    public IReadOnlyList<DocumentAttachmentEntity> Attachments => _attachments;

    private readonly List<DocumentReferralEntity> _referrals = new List<DocumentReferralEntity>();
    public IReadOnlyList<DocumentReferralEntity> Referrals => _referrals;


    protected DocumentAggregateRoot(){}

    public DocumentAggregateRoot(
        string title,
        string content,
        ParticipantId senderUserId,
        ParticipantId reciverUserId,
        DocumentType type,
        int letterNumber)
    {
        DocumentId = DocumentId.NewId();
        Title = title ?? throw new ArgumentNullException(nameof(title));
        Content = content ?? throw new ArgumentNullException(nameof(content));
        OwnerParticipantId = senderUserId ?? throw new ArgumentNullException(nameof(senderUserId));
        ReceiverParticipantId = reciverUserId ?? throw new ArgumentNullException(nameof(reciverUserId));
        Type = type;
        Status = DocumentStatus.Draft;

        LetterDate = DateTime.UtcNow; // Automatically set to current date
        LetterNumber = letterNumber > 0 ? letterNumber : throw new ArgumentException("Letter number must be greater than zero.", nameof(letterNumber));
        TrackingCode = GenerateTrackingCode(LetterDate, letterNumber);

        AddInitialReferral(ReceiverParticipantId, OwnerParticipantId);
    }

    private string GenerateTrackingCode(DateTime letterDate, int letterNumber)
    {
        return $"{letterDate:yyyyMMddHHmmss}-{letterNumber}";
    }
    private void EnsureNotArchived()
    {
        if (Status == DocumentStatus.Archive)
            throw new PayehException("Operation cannot be performed on an archived document.");
    }
    public void SetTrackingCode(string trackingCode)
    {
        if (string.IsNullOrWhiteSpace(trackingCode))
        {
            throw new PayehException("Tracking code cannot be null or empty.");
        }

        if (TrackingCode == trackingCode)
        {
            throw new PayehException("The new tracking code must be different from the current one.");
        }

        TrackingCode = trackingCode;
    }

    public void UpdateContent(string newContent, UserId editorId)
    {
        EnsureNotArchived();
        Content = newContent;
        AddDomainEvent(new DocumentContentUpdatedEvent(this.DocumentId));
    }
    public void UpdateLetterDate(DateTime now)
    {
        LetterDate = now;
    }
    public void Publish(ParticipantId participantId)
    {
        EnsureNotArchived();
        if (Status == DocumentStatus.Published)
            throw new PayehException("Document is already published.");

        Status = DocumentStatus.Published;
        AddDomainEvent(new DocumentSentEvent(this.DocumentId));
    }

    public DocumentReferralEntity AddInitialReferral(ParticipantId initialReceiverUserId, ParticipantId createdBy)
    {
        var initialReferral = new DocumentReferralEntity(this.DocumentId, OwnerParticipantId, initialReceiverUserId, null);
        _referrals.Add(initialReferral);

        return initialReferral;
    }

    public void Archive()
    {
        Status = DocumentStatus.Archive;
        AddDomainEvent(new DocumentArchivedEvent(this.DocumentId));
    }

    public void UpdateTitle(string newTitle)
    {
        EnsureNotArchived();
        Title = newTitle;
        AddDomainEvent(new DocumentTitleUpdatedEvent(this.DocumentId));
    }

    public void ChangeType(DocumentType newType)
    {
        EnsureNotArchived();
        Type = newType;
        AddDomainEvent(new DocumentTypeChangedEvent(this.DocumentId));
    }

    public void RevertToDraft()
    {
        if (Status != DocumentStatus.Published && Status != DocumentStatus.Draft)
            throw new PayehException("Only published or draft documents can be reverted to draft.");

        Status = DocumentStatus.Draft;
        AddDomainEvent(new DocumentRevertedToDraftEvent(this.DocumentId));
    }

    public void AddAttachment(string fileName, string fileType, long fileSize, string filePath)
    {
        EnsureNotArchived();
        if (string.IsNullOrWhiteSpace(fileName)) throw new PayehException("File name cannot be empty.");
        if (string.IsNullOrWhiteSpace(fileType)) throw new PayehException("File type cannot be empty.");
        if (fileSize <= 0) throw new PayehException("File size must be greater than zero.");

        var attachment = new DocumentAttachmentEntity(DocumentId, fileName, fileType, fileSize, filePath);
        _attachments.Add(attachment);
        AddDomainEvent(new DocumentAttachmentAddedEvent(this.DocumentId, attachment.DocumentAttachmentId));
    }

    public void RemoveAttachment(DocumentAttachmentId attachmentId, UserId UserId)
    {
        EnsureNotArchived();
        var attachment = _attachments.FirstOrDefault(a => a.DocumentAttachmentId == attachmentId);
        if (attachment == null)
            throw new PayehException("Attachment not found.");

        _attachments.Remove(attachment);
        AddDomainEvent(new DocumentAttachmentRemovedEvent(this.DocumentId, attachmentId));
    }

    public void UpdateAttachment(DocumentAttachmentId attachmentId, string newFileName, string newFileType,
        long newFileSize, string newFilePath, UserId UserId)
    {
        EnsureNotArchived();
        var attachment = _attachments.FirstOrDefault(a => a.DocumentAttachmentId == attachmentId);
        if (attachment == null)
            throw new PayehException("Attachment not found.");

        attachment.UpdateFileInfo(newFileName, newFileType, newFileSize, newFilePath);
        AddDomainEvent(new DocumentAttachmentUpdatedEvent(this.DocumentId, attachmentId));
    }

    public DocumentReferralEntity AddReferral(
        ParticipantId receiverUserId,
        ParticipantId creatorId,
        string? content = null,
        DocumentReferralId? parentReferralId = null)
    {
        if (Status != DocumentStatus.Published)
            throw new PayehException("Referrals can only be added to published documents.");

        var parentReferral = parentReferralId != null
            ? _referrals.FirstOrDefault(r => r.DocumentReferralId == parentReferralId)
            : null;

        if (parentReferralId != null && parentReferral == null)
            throw new PayehException("Parent referral not found or invalid.");

        var referral = new DocumentReferralEntity(this.DocumentId, creatorId, receiverUserId,content, parentReferralId);
        _referrals.Add(referral);

        return referral;
    }


    public void RespondToReferral(DocumentReferralId referralId, UserId responderId)
    {
        var referral = _referrals.FirstOrDefault(r => r.DocumentReferralId == referralId)
                       ?? throw new PayehException("Referral not found.");

        if (referral.Status == ReferralStatus.Canceled)
            throw new PayehException("Referral is canceled.");

        if (referral.Status == ReferralStatus.Responded)
            throw new PayehException("Referral already responded.");

        referral.Respond();
        AddDomainEvent(new DocumentReferralRespondedEvent(this.DocumentId, referralId));
    }

    public IEnumerable<DocumentReferralEntity> GetActiveReferrals()
    {
        return _referrals.Where(r => r.Status == ReferralStatus.Pending);
    }


    public override object GetKey()
    {
        return DocumentId;
    }

    public override string ToString()
    {
        var documentSchema = new StringBuilder();
        documentSchema.AppendLine("=== Document Summary ===");
        documentSchema.AppendLine($"Title: {Title}");
        documentSchema.AppendLine($"Content: {Content}");
        documentSchema.AppendLine($"Type: {Type}");
        documentSchema.AppendLine($"Status: {Status}");
        documentSchema.AppendLine($"Sender: {OwnerParticipantId}");
        documentSchema.AppendLine($"Attachments: {Attachments.Count}");
        documentSchema.AppendLine($"Referrals: {Referrals.Count}");

        return documentSchema.ToString();
    }
}
