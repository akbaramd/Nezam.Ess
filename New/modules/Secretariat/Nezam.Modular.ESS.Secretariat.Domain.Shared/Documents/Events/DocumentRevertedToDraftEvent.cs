using Nezam.Modular.ESS.Secretariat.Domain.Shared.Documents.ValueObjects;
using Payeh.SharedKernel.Domain.DomainEvents;

namespace Nezam.Modular.ESS.Secretariat.Domain.Shared.Documents.Events;

public class DocumentRevertedToDraftEvent : DomainEvent
{
    public DocumentId DocumentId { get; }

    public DocumentRevertedToDraftEvent(DocumentId documentId)
    {
        DocumentId = documentId;
    }
}