using Payeh.SharedKernel.Domain.DomainEvents;

namespace Nezam.EEs.Shared.Domain.Identity.User.DomainEvents;

public class UserNameUpdatedEvent : DomainEvent
{
    public UserId UserId { get; }
    public string NewUserName { get; }

    public UserNameUpdatedEvent(UserId userId, string newUserName)
    {
        UserId = userId;
        NewUserName = newUserName;
    }
}