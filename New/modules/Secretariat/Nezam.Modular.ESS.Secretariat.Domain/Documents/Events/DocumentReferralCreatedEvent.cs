using Bonyan.Layer.Domain.Events;
using Bonyan.UserManagement.Domain.ValueObjects;

namespace Nezam.Modular.ESS.Secretariat.Domain.Documents;

public class DocumentReferralCreatedEvent : DomainEventBase
{
    public DocumentReferralCreatedEvent(DocumentId id, DocumentReferralId referralId, UserId receiverUserId,UserId referrerUserId)
    {
    }
}