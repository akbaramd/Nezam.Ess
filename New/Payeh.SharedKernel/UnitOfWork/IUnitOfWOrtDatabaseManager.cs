using Payeh.SharedKernel.Domain.DomainEvents;

namespace Payeh.SharedKernel.UnitOfWork;

public interface IUnitOfWOrtDatabaseManager
{
    Task SaveChangesAsync(CancellationToken cancellationToken = default);
    IEnumerable<IDomainEvent> GetDomainEvents();
    void ClearDomainEvents
        ();
}