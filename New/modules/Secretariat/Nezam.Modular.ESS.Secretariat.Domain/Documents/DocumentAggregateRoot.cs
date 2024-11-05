using System.Collections.Generic;
using System.Linq;
using Bonyan.Layer.Domain.Aggregates;
using Bonyan.UserManagement.Domain.ValueObjects;
using Nezam.Modular.ESS.Identity.Domain.User;
using Nezam.Modular.ESS.Secretariat.Domain.Documents.Events;
using System;

namespace Nezam.Modular.ESS.Secretariat.Domain.Documents;

public class DocumentAggregateRoot : FullAuditableAggregateRoot<DocumentId>
{
    public string Title { get; private set; }
    public string Content { get; private set; }
    public UserId SenderUserId { get; private set; }
    public UserEntity SenderUser { get; private set; }
    public DocumentType Type { get; private set; }
    public DocumentStatus Status { get; private set; }

    // Replace IReadOnlyCollection with List for EF Core compatibility
    private readonly List<DocumentAttachmentEntity> _attachments = new List<DocumentAttachmentEntity>();
    public IReadOnlyList<DocumentAttachmentEntity> Attachments => _attachments;

    private readonly List<DocumentReferralEntity> _referrals = new List<DocumentReferralEntity>();
    public IReadOnlyList<DocumentReferralEntity> Referrals => _referrals;

    public DocumentAggregateRoot(string title, string content, UserId senderUserId, DocumentType type)
    {
        Title = title;
        Content = content;
        SenderUserId = senderUserId;
        Type = type;
        Status = DocumentStatus.Draft; // Default status when a document is created
    }

    // Behavior to Update Document Content
    public void UpdateContent(string newContent)
    {
        if (Status == DocumentStatus.Archive)
            throw new InvalidOperationException("Cannot update content of an archived document.");

        Content = newContent;
        AddDomainEvent(new DocumentContentUpdatedEvent(this.Id));
    }

    // Behavior to Send Document
    public void Send()
    {
        if (Status == DocumentStatus.Send)
            throw new InvalidOperationException("Document is already sent.");
        if (Status == DocumentStatus.Archive)
            throw new InvalidOperationException("Cannot send an archived document.");

        Status = DocumentStatus.Send;
        AddDomainEvent(new DocumentSentEvent(this.Id));
    }

    // Behavior to Archive Document
    public void Archive()
    {
        if (Status == DocumentStatus.Archive)
            throw new InvalidOperationException("Document is already archived.");

        Status = DocumentStatus.Archive;
        AddDomainEvent(new DocumentArchivedEvent(this.Id));
    }

    // Behavior to Update Title
    public void UpdateTitle(string newTitle)
    {
        if (Status == DocumentStatus.Archive)
            throw new InvalidOperationException("Cannot update title of an archived document.");

        Title = newTitle;
        AddDomainEvent(new DocumentTitleUpdatedEvent(this.Id));
    }

    // Behavior to Change Document Type (e.g., Incoming, Outgoing, Internal)
    public void ChangeType(DocumentType newType)
    {
        if (Status == DocumentStatus.Archive)
            throw new InvalidOperationException("Cannot change the type of an archived document.");

        Type = newType;
        AddDomainEvent(new DocumentTypeChangedEvent(this.Id));
    }

    // Additional behavior: Revert Document to Draft (optional)
    public void RevertToDraft()
    {
        if (Status == DocumentStatus.Archive)
            throw new InvalidOperationException("Cannot revert an archived document to draft.");

        Status = DocumentStatus.Draft;
        AddDomainEvent(new DocumentRevertedToDraftEvent(this.Id));
    }

    // Attachment-related behaviors
    public void AddAttachment(string fileName, string fileType, long fileSize, string filePath)
    {
        if (Status == DocumentStatus.Archive)
            throw new InvalidOperationException("Cannot add attachments to an archived document.");

        var attachment = new DocumentAttachmentEntity(fileName, fileType, fileSize, filePath);
        _attachments.Add(attachment);

        AddDomainEvent(new DocumentAttachmentAddedEvent(this.Id, attachment.Id));
    }

    public void RemoveAttachment(DocumentAttachmentId attachmentId)
    {
        var attachment = _attachments.FirstOrDefault(a => a.Id == attachmentId);
        if (attachment == null)
            throw new InvalidOperationException("Attachment not found.");

        _attachments.Remove(attachment);
        AddDomainEvent(new DocumentAttachmentRemovedEvent(this.Id, attachmentId));
    }

    public void UpdateAttachment(DocumentAttachmentId attachmentId, string newFileName, string newFileType, long newFileSize, string newFilePath)
    {
        var attachment = _attachments.FirstOrDefault(a => a.Id == attachmentId);
        if (attachment == null)
            throw new InvalidOperationException("Attachment not found.");

        attachment.UpdateFileInfo(newFileName, newFileType, newFileSize, newFilePath);
        AddDomainEvent(new DocumentAttachmentUpdatedEvent(this.Id, attachmentId));
    }

    // Behavior to Add a Referral
    public DocumentReferralEntity AddReferral(UserId referrerUserId, UserId receiverUserId)
    {
        var referral = new DocumentReferralEntity(this.Id, referrerUserId, receiverUserId);
        _referrals.Add(referral);

        AddDomainEvent(new DocumentReferralCreatedEvent(this.Id, referral.Id, receiverUserId, referrerUserId));
        return referral;
    }

    // Behavior to Set Next Referral in Pipeline (Sequential Workflow)
    public void SetNextReferral(DocumentReferralId currentReferralId, DocumentReferralId nextReferralId)
    {
        var currentReferral = _referrals.FirstOrDefault(r => r.Id == currentReferralId);
        if (currentReferral == null)
            throw new InvalidOperationException("Current referral not found.");

        currentReferral.SetNextReferral(nextReferralId);
    }

    // Behavior to Mark Referral as Viewed
    public void MarkReferralAsViewed(DocumentReferralId referralId)
    {
        var referral = _referrals.FirstOrDefault(r => r.Id == referralId);
        if (referral == null)
            throw new InvalidOperationException("Referral not found.");

        referral.MarkAsViewed();
    }

    public void RespondToReferral(DocumentReferralId referralId, string responseContent)
    {
        // یافتن ارجاعی که باید به آن پاسخ داده شود
        var referral = _referrals.FirstOrDefault(r => r.Id == referralId);
        if (referral == null)
            throw new InvalidOperationException("Referral not found.");

        // اگر این ارجاع یا ارجاع موازی دیگری در این مرحله پاسخ داده نشده باشد، اجازه پاسخ‌گویی می‌دهیم
        if (_referrals.Any(r => r.DocumentId == referral.DocumentId && r.Status == ReferralStatus.New && r.Id != referralId))
        {
            // ارجاعات موازی دیگر که جدید هستند، لغو می‌شوند
            foreach (var otherReferral in _referrals.Where(r => r.DocumentId == referral.DocumentId && r.Status == ReferralStatus.New && r.Id != referralId))
            {
                otherReferral.Cancel();
            }
        }
    
        // ثبت پاسخ در ارجاع فعلی
        referral.Respond(responseContent);
    }



    // Behavior to Get Active Referrals (For parallel processing)
    public IEnumerable<DocumentReferralEntity> GetActiveReferrals()
    {
        return _referrals.Where(r => !r.IsProcessed());
    }
}
