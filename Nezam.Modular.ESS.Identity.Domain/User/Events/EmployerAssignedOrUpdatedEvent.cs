using Bonyan.Layer.Domain.Events;
using Bonyan.UserManagement.Domain.ValueObjects;
using Nezam.Modular.ESS.IdEntity.Domain.Employer;

namespace Nezam.Modular.ESS.IdEntity.Domain.User.Events;

public class EmployerAssignedOrUpdatedEvent : BonDomainEventBase
{
    public BonUserId UserId { get; }
    public EmployerId EmployerId { get; }
    public DateTime Timestamp { get; }

    public EmployerAssignedOrUpdatedEvent(BonUserId userId, EmployerId employerId)
    {
        UserId = userId;
        EmployerId = employerId;
        Timestamp = DateTime.UtcNow;
    }
}