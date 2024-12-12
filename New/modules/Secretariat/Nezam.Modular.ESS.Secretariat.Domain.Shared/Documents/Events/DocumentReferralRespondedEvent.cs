using Nezam.Modular.ESS.Secretariat.Domain.Shared.Documents.ValueObjects;
using Payeh.SharedKernel.Domain.DomainEvents;

namespace Nezam.Modular.ESS.Secretariat.Domain.Shared.Documents.Events;

public class DocumentReferralRespondedEvent : DomainEvent
{
    public DocumentReferralRespondedEvent(DocumentId id, DocumentReferralId referralId)
    {
        
    }
}