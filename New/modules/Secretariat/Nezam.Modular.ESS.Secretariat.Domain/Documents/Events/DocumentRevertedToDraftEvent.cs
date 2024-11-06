using Bonyan.Layer.Domain.Events;
using Nezam.Modular.ESS.Secretariat.Domain.Documents.ValueObjects;

namespace Nezam.Modular.ESS.Secretariat.Domain.Documents.Events;

public class DocumentRevertedToDraftEvent : DomainEventBase
{
    public DocumentId DocumentId { get; }

    public DocumentRevertedToDraftEvent(DocumentId documentId)
    {
        DocumentId = documentId;
    }
}