using Payeh.SharedKernel.Domain.DomainEvents;

namespace Nezam.EEs.Shared.Domain.Identity.User.DomainEvents;

public class UserEmailUpdatedEvent : DomainEvent
{
    public UserId UserId { get; }
    public string? NewEmail { get; }

    public UserEmailUpdatedEvent(UserId userId, string? newEmail)
    {
        UserId = userId;
        NewEmail = newEmail;
    }
}