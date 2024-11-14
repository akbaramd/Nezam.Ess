using Bonyan.Layer.Domain.Events;
using Nezam.Modular.ESS.Secretariat.Domain.Shared.Documents.ValueObjects;

namespace Nezam.Modular.ESS.Secretariat.Domain.Shared.Documents.Events;

public class DocumentTitleUpdatedEvent : BonDomainEventBase
{
    public DocumentId DocumentId { get; }

    public DocumentTitleUpdatedEvent(DocumentId documentId)
    {
        DocumentId = documentId;
    }
}