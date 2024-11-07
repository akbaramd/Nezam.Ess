using Bonyan.Layer.Domain.Events;
using Nezam.Modular.ESS.Secretariat.Domain.Documents.ValueObjects;

namespace Nezam.Modular.ESS.Secretariat.Domain.Documents.Events;

public class DocumentAttachmentRemovedEvent : BonDomainEventBase
{
    public DocumentId DocumentId { get; }
    public DocumentAttachmentId AttachmentId { get; }

    public DocumentAttachmentRemovedEvent(DocumentId documentId, DocumentAttachmentId attachmentId)
    {
        DocumentId = documentId;
        AttachmentId = attachmentId;
    }
}