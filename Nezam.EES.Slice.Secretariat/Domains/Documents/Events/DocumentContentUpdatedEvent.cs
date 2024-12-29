using Nezam.EES.Slice.Secretariat.Domains.Documents.ValueObjects;
using Payeh.SharedKernel.Domain.DomainEvents;

namespace Nezam.EES.Slice.Secretariat.Domains.Documents.Events;

public class DocumentContentUpdatedEvent : DomainEvent
{
    public DocumentId DocumentId { get; }

    public DocumentContentUpdatedEvent(DocumentId documentId)
    {
        DocumentId = documentId;
    }
}