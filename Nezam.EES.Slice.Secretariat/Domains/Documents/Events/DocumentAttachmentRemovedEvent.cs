using Nezam.EES.Slice.Secretariat.Domains.Documents.ValueObjects;
using Payeh.SharedKernel.Domain.DomainEvents;

namespace Nezam.EES.Slice.Secretariat.Domains.Documents.Events;

public class DocumentAttachmentRemovedEvent : DomainEvent
{
    public DocumentId DocumentId { get; }
    public DocumentAttachmentId AttachmentId { get; }

    public DocumentAttachmentRemovedEvent(DocumentId documentId, DocumentAttachmentId attachmentId)
    {
        DocumentId = documentId;
        AttachmentId = attachmentId;
    }
}