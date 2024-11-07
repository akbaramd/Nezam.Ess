using Bonyan.Layer.Domain.Events;
using Bonyan.UserManagement.Domain.ValueObjects;

namespace Nezam.Modular.ESS.IdEntity.Domain.User.Events;

public class ContactInfoUpdatedEvent : BonDomainEventBase
{
    public BonUserId UserId { get; }
    public string Email { get; }
    public string PhoneNumber { get; }
    public DateTime Timestamp { get; }

    public ContactInfoUpdatedEvent(BonUserId userId, string email, string phoneNumber)
    {
        UserId = userId;
        Email = email;
        PhoneNumber = phoneNumber;
        Timestamp = DateTime.UtcNow;
    }
}