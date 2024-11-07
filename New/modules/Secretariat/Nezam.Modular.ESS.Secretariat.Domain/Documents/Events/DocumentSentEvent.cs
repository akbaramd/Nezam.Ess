using Bonyan.Layer.Domain.Events;
using Nezam.Modular.ESS.Secretariat.Domain.Documents.ValueObjects;

namespace Nezam.Modular.ESS.Secretariat.Domain.Documents.Events;

public class DocumentSentEvent : BonDomainEventBase
{
    public DocumentId DocumentId { get; }

    public DocumentSentEvent(DocumentId documentId)
    {
        DocumentId = documentId;
    }
}