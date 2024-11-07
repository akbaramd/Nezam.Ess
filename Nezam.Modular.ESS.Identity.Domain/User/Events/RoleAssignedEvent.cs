using Bonyan.Layer.Domain.Events;
using Bonyan.UserManagement.Domain.ValueObjects;

namespace Nezam.Modular.ESS.IdEntity.Domain.User.Events;

public class RoleAssignedEvent : BonDomainEventBase
{
    public BonUserId UserId { get; }
    public string RoleName { get; }
    public DateTime Timestamp { get; }

    public RoleAssignedEvent(BonUserId userId, string roleName)
    {
        UserId = userId;
        RoleName = roleName;
        Timestamp = DateTime.UtcNow;
    }
}