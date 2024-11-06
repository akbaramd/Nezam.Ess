using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bonyan.Layer.Domain.Aggregates;
using Bonyan.UserManagement.Domain.ValueObjects;
using Nezam.Modular.ESS.Identity.Domain.User;
using Nezam.Modular.ESS.Secretariat.Domain.Documents;
using Nezam.Modular.ESS.Secretariat.Domain.Documents.Enumerations;
using Nezam.Modular.ESS.Secretariat.Domain.Documents.Events;
using Nezam.Modular.ESS.Secretariat.Domain.Documents.Exceptions;
using Nezam.Modular.ESS.Secretariat.Domain.Documents.ValueObjects;

public class DocumentAggregateRoot : FullAuditableAggregateRoot<DocumentId>
{
    public string Title { get; private set; }
    public string Content { get; private set; }
    public UserId SenderUserId { get; private set; }
    public UserEntity SenderUser { get; private set; }
    public DocumentType Type { get; private set; }
    public DocumentStatus Status { get; private set; }

    private readonly List<DocumentAttachmentEntity> _attachments = new List<DocumentAttachmentEntity>();
    public IReadOnlyList<DocumentAttachmentEntity> Attachments => _attachments;

    private readonly List<DocumentReferralEntity> _referrals = new List<DocumentReferralEntity>();
    public IReadOnlyList<DocumentReferralEntity> Referrals => _referrals;

    private readonly List<DocumentVersion> _versions = new List<DocumentVersion>();
    public IReadOnlyList<DocumentVersion> Versions => _versions;

    private readonly List<DocumentActivityLog> _activityLogs = new List<DocumentActivityLog>();
    public IReadOnlyList<DocumentActivityLog> ActivityLogs => _activityLogs;

    public DocumentAggregateRoot(string title, string content, UserId senderUserId, DocumentType type)
    {
        Id = DocumentId.CreateNew();
        Title = title ?? throw new ArgumentNullException(nameof(title));
        Content = content ?? throw new ArgumentNullException(nameof(content));
        SenderUserId = senderUserId ?? throw new ArgumentNullException(nameof(senderUserId));
        Type = type;
        Status = DocumentStatus.Draft;

        LogActivity(senderUserId, "Document created");
        AddVersion(senderUserId, title,content); // Capture initial version
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
        LogActivity(editorId, "Content updated");
        AddVersion(editorId,Title, newContent); // Capture version for content update
        AddDomainEvent(new DocumentContentUpdatedEvent(this.Id));
    }

    public void Publish(UserId userId)
    {
        EnsureNotArchived();
        if (Status == DocumentStatus.Published)
            throw new InvalidOperationException("Document is already published.");

        Status = DocumentStatus.Published;
        LogActivity(userId, "Document published");
        AddDomainEvent(new DocumentSentEvent(this.Id));
    }

    public DocumentReferralEntity AddInitialReferral(UserId initialReceiverUserId, UserId createdBy)
    {
        if (Status != DocumentStatus.Published)
            throw new InvalidOperationException("Cannot add referral to an unpublished document.");

        var initialReferral = new DocumentReferralEntity(this.Id, SenderUserId, initialReceiverUserId, null);
        _referrals.Add(initialReferral);
        LogActivity(createdBy, "Initial referral added");
        AddDomainEvent(new DocumentReferralCreatedEvent(this.Id, initialReferral.Id, initialReceiverUserId, SenderUserId));

        return initialReferral;
    }

    public void Archive(UserId userId)
    {
        Status = DocumentStatus.Archive;
        LogActivity(userId, "Document archived");
        AddDomainEvent(new DocumentArchivedEvent(this.Id));
    }

    public void UpdateTitle(string newTitle, UserId editorId)
    {
        EnsureNotArchived();
        Title = newTitle;
        LogActivity(editorId, "Title updated");
        AddVersion(editorId, newTitle,Content); // Capture version for title update
        AddDomainEvent(new DocumentTitleUpdatedEvent(this.Id));
    }

    public void ChangeType(DocumentType newType, UserId userId)
    {
        EnsureNotArchived();
        Type = newType;
        LogActivity(userId, "Document type changed");
        AddVersion(userId,Title, Content); // Capture version for type change
        AddDomainEvent(new DocumentTypeChangedEvent(this.Id));
    }

    public void RevertToDraft(UserId userId)
    {
        if (Status != DocumentStatus.Published && Status != DocumentStatus.Draft)
            throw new InvalidOperationException("Only published or draft documents can be reverted to draft.");

        Status = DocumentStatus.Draft;
        LogActivity(userId, "Document reverted to draft");
        AddDomainEvent(new DocumentRevertedToDraftEvent(this.Id));
    }

    public void AddAttachment(string fileName, string fileType, long fileSize, string filePath, UserId userId)
    {
        EnsureNotArchived();
        if (string.IsNullOrWhiteSpace(fileName)) throw new ArgumentException("File name cannot be empty.");
        if (string.IsNullOrWhiteSpace(fileType)) throw new ArgumentException("File type cannot be empty.");
        if (fileSize <= 0) throw new ArgumentException("File size must be greater than zero.");

        var attachment = new DocumentAttachmentEntity(fileName, fileType, fileSize, filePath);
        _attachments.Add(attachment);
        LogActivity(userId, "Attachment added");
        AddDomainEvent(new DocumentAttachmentAddedEvent(this.Id, attachment.Id));
    }

    public void RemoveAttachment(DocumentAttachmentId attachmentId, UserId userId)
    {
        EnsureNotArchived();
        var attachment = _attachments.FirstOrDefault(a => a.Id == attachmentId);
        if (attachment == null)
            throw new InvalidOperationException("Attachment not found.");

        _attachments.Remove(attachment);
        LogActivity(userId, "Attachment removed");
        AddDomainEvent(new DocumentAttachmentRemovedEvent(this.Id, attachmentId));
    }

    public void UpdateAttachment(DocumentAttachmentId attachmentId, string newFileName, string newFileType,
        long newFileSize, string newFilePath, UserId userId)
    {
        EnsureNotArchived();
        var attachment = _attachments.FirstOrDefault(a => a.Id == attachmentId);
        if (attachment == null)
            throw new InvalidOperationException("Attachment not found.");

        attachment.UpdateFileInfo(newFileName, newFileType, newFileSize, newFilePath);
        LogActivity(userId, "Attachment updated");
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
        LogActivity(createdBy, "Referral added");
        AddDomainEvent(new DocumentReferralCreatedEvent(this.Id, referral.Id, receiverUserId, parentReferral.ReceiverUserId));

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
        LogActivity(responderId, "Referral responded");
        AddDomainEvent(new DocumentReferralRespondedEvent(this.Id, referralId));
    }

    public IEnumerable<DocumentReferralEntity> GetActiveReferrals()
    {
        return _referrals.Where(r => r.Status == ReferralStatus.Pending);
    }

    private void AddVersion(UserId editorId,string titleSnapshot, string contentSnapshot)
    {
        var version = new DocumentVersion(_versions.Count + 1, DateTime.UtcNow, editorId,titleSnapshot, contentSnapshot);
        _versions.Add(version);
    }

    private void LogActivity(UserId userId, string description)
    {
        var log = new DocumentActivityLog(DateTime.UtcNow, userId, description);
        _activityLogs.Add(log);
    }

    public override string ToString()
    {
        var documentSchema = new StringBuilder();
        documentSchema.AppendLine("=== Document Summary ===");
        documentSchema.AppendLine($"Title: {Title}");
        documentSchema.AppendLine($"Content: {Content}");
        documentSchema.AppendLine($"Type: {Type}");
        documentSchema.AppendLine($"Status: {Status}");
        documentSchema.AppendLine($"Sender: {SenderUserId}");
        documentSchema.AppendLine($"Attachments: {Attachments.Count}");
        documentSchema.AppendLine($"Referrals: {Referrals.Count}");
        documentSchema.AppendLine($"Versions: {Versions.Count}");
        documentSchema.AppendLine($"Activity Logs: {ActivityLogs.Count}");

        return documentSchema.ToString();
    }
}
