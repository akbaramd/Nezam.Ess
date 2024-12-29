using Nezam.EES.Slice.Secretariat.Domains.Documents.ValueObjects;
using Payeh.SharedKernel.Domain.DomainEvents;

namespace Nezam.EES.Slice.Secretariat.Domains.Documents.Events;

public class DocumentArchivedEvent : DomainEvent
{
    public DocumentId DocumentId { get; }

    public DocumentArchivedEvent(DocumentId documentId)
    {
        DocumentId = documentId;
    }
}