using Bonyan.Layer.Domain.Events;
using Nezam.Modular.ESS.Secretariat.Domain.Documents.ValueObjects;

namespace Nezam.Modular.ESS.Secretariat.Domain.Documents.Events;

public class DocumentTitleUpdatedEvent : DomainEventBase
{
    public DocumentId DocumentId { get; }

    public DocumentTitleUpdatedEvent(DocumentId documentId)
    {
        DocumentId = documentId;
    }
}