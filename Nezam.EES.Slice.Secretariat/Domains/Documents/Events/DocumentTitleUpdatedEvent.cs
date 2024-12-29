using Nezam.EES.Slice.Secretariat.Domains.Documents.ValueObjects;
using Payeh.SharedKernel.Domain.DomainEvents;

namespace Nezam.EES.Slice.Secretariat.Domains.Documents.Events;

public class DocumentTitleUpdatedEvent : DomainEvent
{
    public DocumentId DocumentId { get; }

    public DocumentTitleUpdatedEvent(DocumentId documentId)
    {
        DocumentId = documentId;
    }
}