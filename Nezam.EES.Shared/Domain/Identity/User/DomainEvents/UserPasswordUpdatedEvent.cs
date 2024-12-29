using Payeh.SharedKernel.Domain.DomainEvents;

namespace Nezam.EEs.Shared.Domain.Identity.User.DomainEvents;

public class UserPasswordUpdatedEvent : DomainEvent
{
    public UserId UserId { get; }

    public UserPasswordUpdatedEvent(UserId userId)
    {
        UserId = userId;
    }
}