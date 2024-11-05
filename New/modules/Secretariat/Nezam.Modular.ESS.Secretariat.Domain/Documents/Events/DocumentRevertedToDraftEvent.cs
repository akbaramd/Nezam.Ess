using Bonyan.Layer.Domain.Events;

namespace Nezam.Modular.ESS.Secretariat.Domain.Documents.Events;

public class DocumentRevertedToDraftEvent : DomainEventBase
{
    public DocumentId DocumentId { get; }

    public DocumentRevertedToDraftEvent(DocumentId documentId)
    {
        DocumentId = documentId;
    }
}