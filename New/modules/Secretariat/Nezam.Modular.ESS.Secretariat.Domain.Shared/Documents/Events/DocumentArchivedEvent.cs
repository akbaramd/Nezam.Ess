using Nezam.Modular.ESS.Secretariat.Domain.Shared.Documents.ValueObjects;
using Payeh.SharedKernel.Domain.DomainEvents;

namespace Nezam.Modular.ESS.Secretariat.Domain.Shared.Documents.Events;

public class DocumentArchivedEvent : DomainEvent
{
    public DocumentId DocumentId { get; }

    public DocumentArchivedEvent(DocumentId documentId)
    {
        DocumentId = documentId;
    }
}