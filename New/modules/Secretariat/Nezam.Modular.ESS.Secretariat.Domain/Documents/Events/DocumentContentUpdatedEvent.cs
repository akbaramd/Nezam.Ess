using Bonyan.Layer.Domain.Events;

namespace Nezam.Modular.ESS.Secretariat.Domain.Documents.Events;

public class DocumentContentUpdatedEvent : DomainEventBase
{
    public DocumentId DocumentId { get; }

    public DocumentContentUpdatedEvent(DocumentId documentId)
    {
        DocumentId = documentId;
    }
}