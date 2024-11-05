using Bonyan.Layer.Domain.Events;

namespace Nezam.Modular.ESS.Secretariat.Domain.Documents.Events;

public class DocumentAttachmentRemovedEvent : DomainEventBase
{
    public DocumentId DocumentId { get; }
    public DocumentAttachmentId AttachmentId { get; }

    public DocumentAttachmentRemovedEvent(DocumentId documentId, DocumentAttachmentId attachmentId)
    {
        DocumentId = documentId;
        AttachmentId = attachmentId;
    }
}