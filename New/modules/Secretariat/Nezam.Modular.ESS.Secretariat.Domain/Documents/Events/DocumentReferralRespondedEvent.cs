using Bonyan.Layer.Domain.Events;
using Nezam.Modular.ESS.Secretariat.Domain.Documents.ValueObjects;

namespace Nezam.Modular.ESS.Secretariat.Domain.Documents.Events;

public class DocumentReferralRespondedEvent : DomainEventBase
{
    public DocumentReferralRespondedEvent(DocumentId id, DocumentReferralId referralId)
    {
        
    }
}