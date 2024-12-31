using Payeh.SharedKernel.Domain.DomainEvents;

namespace Payeh.SharedKernel.UnitOfWork.Null;

public class NullUnitOfWOrtDatabaseManager : IUnitOfWOrtDatabaseManager
{
    public static readonly NullUnitOfWOrtDatabaseManager? Instance = new NullUnitOfWOrtDatabaseManager();
    private NullUnitOfWOrtDatabaseManager() { }

    public Task SaveChangesAsync(CancellationToken cancellationToken = default) => Task.CompletedTask;

    public IEnumerable<IDomainEvent> GetDomainEvents()
    {
        // Return an empty collection as there are no domain events in the NullDataStorage
        return Enumerable.Empty<IDomainEvent>();
    }

    public void ClearDomainEvents()
    {
        // Do nothing, as there are no domain events to clear
    }

    public void Dispose()
    {
        
    }
}