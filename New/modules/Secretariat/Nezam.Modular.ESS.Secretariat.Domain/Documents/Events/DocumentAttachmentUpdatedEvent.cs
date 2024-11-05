using Bonyan.Layer.Domain.Events;

namespace Nezam.Modular.ESS.Secretariat.Domain.Documents.Events;

public class DocumentAttachmentUpdatedEvent : DomainEventBase
{
    public DocumentId DocumentId { get; }
    public DocumentAttachmentId AttachmentId { get; }

    public DocumentAttachmentUpdatedEvent(DocumentId documentId, DocumentAttachmentId attachmentId)
    {
        DocumentId = documentId;
        AttachmentId = attachmentId;
    }
}