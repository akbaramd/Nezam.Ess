using Bonyan.Layer.Domain.Events;
using Bonyan.UserManagement.Domain.ValueObjects;
using Nezam.Modular.ESS.Secretariat.Domain.Documents.ValueObjects;

namespace Nezam.Modular.ESS.Secretariat.Domain.Documents.Events;

public class DocumentReferralCreatedEvent : DomainEventBase
{
    public DocumentReferralCreatedEvent(DocumentId id, DocumentReferralId referralId, UserId receiverUserId,UserId referrerUserId)
    {
    }
}