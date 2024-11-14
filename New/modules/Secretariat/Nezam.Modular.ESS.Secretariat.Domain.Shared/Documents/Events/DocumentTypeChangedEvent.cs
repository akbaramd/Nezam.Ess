using Bonyan.Layer.Domain.Events;
using Nezam.Modular.ESS.Secretariat.Domain.Shared.Documents.ValueObjects;

namespace Nezam.Modular.ESS.Secretariat.Domain.Shared.Documents.Events;

public class DocumentTypeChangedEvent : BonDomainEventBase
{
    public DocumentId DocumentId { get; }

    public DocumentTypeChangedEvent(DocumentId documentId)
    {
        DocumentId = documentId;
    }
}