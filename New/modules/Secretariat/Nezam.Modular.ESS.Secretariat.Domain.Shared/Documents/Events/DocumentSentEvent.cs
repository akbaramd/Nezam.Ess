using Nezam.Modular.ESS.Secretariat.Domain.Shared.Documents.ValueObjects;
using Payeh.SharedKernel.Domain.DomainEvents;

namespace Nezam.Modular.ESS.Secretariat.Domain.Shared.Documents.Events;

public class DocumentSentEvent : DomainEvent
{
    public DocumentId DocumentId { get; }

    public DocumentSentEvent(DocumentId documentId)
    {
        DocumentId = documentId;
    }
}