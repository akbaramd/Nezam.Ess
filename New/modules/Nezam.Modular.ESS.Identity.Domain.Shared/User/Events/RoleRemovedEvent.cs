using Bonyan.Layer.Domain.Events;
using Bonyan.UserManagement.Domain.ValueObjects;
using Nezam.Modular.ESS.Identity.Domain.Shared.Roles;

namespace Nezam.Modular.ESS.Identity.Domain.Shared.User.Events;

public class RoleRemovedEvent : BonDomainEventBase
{
    public BonUserId UserId { get; }
    public RoleId RoleName { get; }
    public DateTime Timestamp { get; }

    public RoleRemovedEvent(BonUserId userId, RoleId roleName)
    {
        UserId = userId;
        RoleName = roleName;
        Timestamp = DateTime.UtcNow;
    }
}