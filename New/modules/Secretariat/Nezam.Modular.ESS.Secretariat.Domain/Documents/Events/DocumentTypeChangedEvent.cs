using Bonyan.Layer.Domain.Events;

namespace Nezam.Modular.ESS.Secretariat.Domain.Documents.Events;

public class DocumentTypeChangedEvent : DomainEventBase
{
    public DocumentId DocumentId { get; }

    public DocumentTypeChangedEvent(DocumentId documentId)
    {
        DocumentId = documentId;
    }
}