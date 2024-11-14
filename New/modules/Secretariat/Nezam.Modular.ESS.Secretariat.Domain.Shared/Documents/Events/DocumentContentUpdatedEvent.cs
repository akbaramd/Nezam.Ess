using Bonyan.Layer.Domain.Events;
using Nezam.Modular.ESS.Secretariat.Domain.Shared.Documents.ValueObjects;

namespace Nezam.Modular.ESS.Secretariat.Domain.Shared.Documents.Events;

public class DocumentContentUpdatedEvent : BonDomainEventBase
{
    public DocumentId DocumentId { get; }

    public DocumentContentUpdatedEvent(DocumentId documentId)
    {
        DocumentId = documentId;
    }
}