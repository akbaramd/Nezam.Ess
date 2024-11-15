using Bonyan.Messaging.Abstractions;
using Bonyan.UserManagement.Domain.Events;

namespace Nezam.Modular.ESS.Secretariat.Domain.Documents.DomainHandlers;

public class DocumentUserCreatedConsumer : IBonMessageConsumer<UserCreatedDomainEvent>
{
    public Task ConsumeAsync(UserCreatedDomainEvent domainEvent, CancellationToken? cancellationToken = null)
    {
        Console.Write(domainEvent.User.UserName);
        return Task.CompletedTask;
    }
}