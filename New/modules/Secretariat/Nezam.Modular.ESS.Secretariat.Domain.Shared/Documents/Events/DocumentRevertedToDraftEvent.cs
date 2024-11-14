using Bonyan.Layer.Domain.Events;
using Nezam.Modular.ESS.Secretariat.Domain.Shared.Documents.ValueObjects;

namespace Nezam.Modular.ESS.Secretariat.Domain.Shared.Documents.Events;

public class DocumentRevertedToDraftEvent : BonDomainEventBase
{
    public DocumentId DocumentId { get; }

    public DocumentRevertedToDraftEvent(DocumentId documentId)
    {
        DocumentId = documentId;
    }
}