using Bonyan.Layer.Domain.Events;

namespace Nezam.Modular.ESS.Secretariat.Domain.Documents.Events;

public class DocumentSentEvent : DomainEventBase
{
    public DocumentId DocumentId { get; }

    public DocumentSentEvent(DocumentId documentId)
    {
        DocumentId = documentId;
    }
}