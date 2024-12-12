using System.Text;
using Nezam.Modular.ESS.Identity.Domain.Shared.User;
using Nezam.Modular.ESS.Identity.Domain.User;
using Nezam.Modular.ESS.Secretariat.Domain.Documents.Exceptions;
using Nezam.Modular.ESS.Secretariat.Domain.Shared.Documents.Enumerations;
using Nezam.Modular.ESS.Secretariat.Domain.Shared.Documents.Events;
using Nezam.Modular.ESS.Secretariat.Domain.Shared.Documents.ValueObjects;
using Payeh.SharedKernel.Domain;

namespace Nezam.Modular.ESS.Secretariat.Domain.Documents;

public class DocumentAggregateRoot : AggregateRoot
{
    public DocumentId Id { get; set; }
    public string Title { get; private set; }
    public string Content { get; private set; }
    public UserId OwnerUserId { get; private set; }
    public UserEntity OwnerUser { get; private set; }
    
    public DocumentType Type { get; private set; }
    public DocumentStatus Status { get; private set; }

    private readonly List<DocumentAttachmentEntity> _attachments = new List<DocumentAttachmentEntity>();
    public IReadOnlyList<DocumentAttachmentEntity> Attachments => _attachments;

    private readonly List<DocumentReferralEntity> _referrals = new List<DocumentReferralEntity>();
    public IReadOnlyList<DocumentReferralEntity> Referrals => _referrals;

    private readonly List<DocumentActivityLogEntity> _activityLogs = new List<DocumentActivityLogEntity>();
    public IReadOnlyList<DocumentActivityLogEntity> ActivityLogs => _activityLogs;

    protected DocumentAggregateRoot(){}

    public DocumentAggregateRoot(string title, string content, UserId senderUserId, DocumentType type)
    {
        Id = DocumentId.NewId();
        Title = title ?? throw new ArgumentNullException(nameof(title));
        Content = content ?? throw new ArgumentNullException(nameof(content));
        OwnerUserId = senderUserId ?? throw new ArgumentNullException(nameof(senderUserId));
        Type = type;
        Status = DocumentStatus.Draft;

        LogActivity(senderUserId, DocumentActivityConst.DocumentCreated, "Document created");
    }

    private void EnsureNotArchived()
    {
        if (Status == DocumentStatus.Archive)
            throw new InvalidOperationException("Operation cannot be performed on an archived document.");
    }

    public void UpdateContent(string newContent, UserId editorId)
    {
        EnsureNotArchived();
        Content = newContent;
        LogActivity(editorId, DocumentActivityConst.ContentUpdated, "Content updated");
        AddDomainEvent(new DocumentContentUpdatedEvent(this.Id));
    }

    public void Publish(UserId UserId)
    {
        EnsureNotArchived();
        if (Status == DocumentStatus.Published)
            throw new InvalidOperationException("Document is already published.");

        Status = DocumentStatus.Published;
        LogActivity(UserId, DocumentActivityConst.DocumentPublished, "Document published");
        AddDomainEvent(new DocumentSentEvent(this.Id));
    }

    public DocumentReferralEntity AddInitialReferral(UserId initialReceiverUserId, UserId createdBy)
    {
        if (Status != DocumentStatus.Published)
            throw new InvalidOperationException("Cannot add referral to an unpublished document.");

        var initialReferral = new DocumentReferralEntity(this.Id, OwnerUserId, initialReceiverUserId, null);
        _referrals.Add(initialReferral);
        LogActivity(createdBy, DocumentActivityConst.InitialReferralAdded, "Initial referral added");

        return initialReferral;
    }

    public void Archive(UserId UserId)
    {
        Status = DocumentStatus.Archive;
        LogActivity(UserId, DocumentActivityConst.DocumentArchived, "Document archived");
        AddDomainEvent(new DocumentArchivedEvent(this.Id));
    }

    public void UpdateTitle(string newTitle, UserId editorId)
    {
        EnsureNotArchived();
        Title = newTitle;
        LogActivity(editorId, DocumentActivityConst.TitleUpdated, "Title updated");
        AddDomainEvent(new DocumentTitleUpdatedEvent(this.Id));
    }

    public void ChangeType(DocumentType newType, UserId UserId)
    {
        EnsureNotArchived();
        Type = newType;
        LogActivity(UserId, DocumentActivityConst.TypeChanged, "Document type changed");
        AddDomainEvent(new DocumentTypeChangedEvent(this.Id));
    }

    public void RevertToDraft(UserId UserId)
    {
        if (Status != DocumentStatus.Published && Status != DocumentStatus.Draft)
            throw new InvalidOperationException("Only published or draft documents can be reverted to draft.");

        Status = DocumentStatus.Draft;
        LogActivity(UserId, DocumentActivityConst.DocumentRevertedToDraft, "Document reverted to draft");
        AddDomainEvent(new DocumentRevertedToDraftEvent(this.Id));
    }

    public void AddAttachment(string fileName, string fileType, long fileSize, string filePath, UserId UserId)
    {
        EnsureNotArchived();
        if (string.IsNullOrWhiteSpace(fileName)) throw new ArgumentException("File name cannot be empty.");
        if (string.IsNullOrWhiteSpace(fileType)) throw new ArgumentException("File type cannot be empty.");
        if (fileSize <= 0) throw new ArgumentException("File size must be greater than zero.");

        var attachment = new DocumentAttachmentEntity(Id, fileName, fileType, fileSize, filePath);
        _attachments.Add(attachment);
        LogActivity(UserId, DocumentActivityConst.AttachmentAdded, "Attachment added");
        AddDomainEvent(new DocumentAttachmentAddedEvent(this.Id, attachment.Id));
    }

    public void RemoveAttachment(DocumentAttachmentId attachmentId, UserId UserId)
    {
        EnsureNotArchived();
        var attachment = _attachments.FirstOrDefault(a => a.Id == attachmentId);
        if (attachment == null)
            throw new InvalidOperationException("Attachment not found.");

        _attachments.Remove(attachment);
        LogActivity(UserId, DocumentActivityConst.AttachmentRemoved, "Attachment removed");
        AddDomainEvent(new DocumentAttachmentRemovedEvent(this.Id, attachmentId));
    }

    public void UpdateAttachment(DocumentAttachmentId attachmentId, string newFileName, string newFileType,
        long newFileSize, string newFilePath, UserId UserId)
    {
        EnsureNotArchived();
        var attachment = _attachments.FirstOrDefault(a => a.Id == attachmentId);
        if (attachment == null)
            throw new InvalidOperationException("Attachment not found.");

        attachment.UpdateFileInfo(newFileName, newFileType, newFileSize, newFilePath);
        LogActivity(UserId, DocumentActivityConst.AttachmentUpdated, "Attachment updated");
        AddDomainEvent(new DocumentAttachmentUpdatedEvent(this.Id, attachmentId));
    }

    public DocumentReferralEntity AddReferral(DocumentReferralId parentReferralId, UserId receiverUserId, UserId createdBy)
    {
        if (Status != DocumentStatus.Published)
            throw new InvalidOperationException("Referrals can only be added to published documents.");

        var parentReferral = _referrals.FirstOrDefault(r => r.Id == parentReferralId)
                             ?? throw new InvalidOperationException("Parent referral not found or invalid.");

        var referral = new DocumentReferralEntity(this.Id, parentReferral.ReceiverUserId, receiverUserId, parentReferralId);
        _referrals.Add(referral);
        LogActivity(createdBy, DocumentActivityConst.ReferralAdded, "Referral added");

        return referral;
    }

    public void RespondToReferral(DocumentReferralId referralId, string responseContent, UserId responderId)
    {
        var referral = _referrals.FirstOrDefault(r => r.Id == referralId)
                       ?? throw new ReferralNotFoundException(parameters: new { referralId });

        if (referral.Status == ReferralStatus.Canceled)
            throw new ReferralCanceledException(parameters: new { referralId });

        if (referral.Status == ReferralStatus.Responded)
            throw new ReferralAlreadyRespondedException(parameters: new { referralId });

        referral.Respond(responseContent);
        LogActivity(responderId, DocumentActivityConst.ReferralResponded, "Referral responded");
        AddDomainEvent(new DocumentReferralRespondedEvent(this.Id, referralId));
    }

    public IEnumerable<DocumentReferralEntity> GetActiveReferrals()
    {
        return _referrals.Where(r => r.Status == ReferralStatus.Pending);
    }

    private void LogActivity(UserId UserId, string key, string description)
    {
        var log = new DocumentActivityLogEntity(Id, DateTime.UtcNow, UserId, key, description);
        _activityLogs.Add(log);
    }

    public override object GetKey()
    {
        return Id;
    }

    public override string ToString()
    {
        var documentSchema = new StringBuilder();
        documentSchema.AppendLine("=== Document Summary ===");
        documentSchema.AppendLine($"Title: {Title}");
        documentSchema.AppendLine($"Content: {Content}");
        documentSchema.AppendLine($"Type: {Type}");
        documentSchema.AppendLine($"Status: {Status}");
        documentSchema.AppendLine($"Sender: {OwnerUserId}");
        documentSchema.AppendLine($"Attachments: {Attachments.Count}");
        documentSchema.AppendLine($"Referrals: {Referrals.Count}");
        documentSchema.AppendLine($"Activity Logs: {ActivityLogs.Count}");

        return documentSchema.ToString();
    }
}
