using MediatR;
using Payeh.SharedKernel.EntityFrameworkCore;

namespace Payeh.SharedKernel.UnitOfWork.Null
{
    public class NullUnitOfWork : IUnitOfWork
    {
        public Guid Id { get; } = Guid.NewGuid();
        public IUnitOfWorkOptions Options { get; } = new UnitOfWorkOptions();
        public event EventHandler? Disposed;

        // Transactions - Do Nothing
        public void AddTransaction(string key, IUnitofWOrkTransactionManager unitofWOrkTransactionManager) { }
        public IUnitofWOrkTransactionManager? GetTransaction(string key) => NullUnitofWOrkTransactionManager.Instance;
        public IEnumerable<string> GetTransactionKeys() => Array.Empty<string>();
        public void CommitTransaction(string key) { }
        public Task CommitTransactionAsync(string key, CancellationToken cancellationToken = default) => Task.CompletedTask;
        public void RollbackTransaction(string key) { }
        public Task RollbackTransactionAsync(string key, CancellationToken cancellationToken = default) => Task.CompletedTask;

        // Data Storage - Do Nothing
        public void AddDataStorage(string key, IUnitOfWOrtDatabaseManager unitOfWOrtDatabaseManager) { }
        public IUnitOfWOrtDatabaseManager? GetDataStorage(string key) => NullUnitOfWOrtDatabaseManager.Instance;
        public IEnumerable<string> GetDataStorageKeys() => Array.Empty<string>();

        // Global Operations - Do Nothing
        public void CommitAll() { }
        public Task CommitAsync(CancellationToken cancellationToken = default) => Task.CompletedTask;
        public void RollbackAll() { }
        public Task RollbackAsync(CancellationToken cancellationToken = default) => Task.CompletedTask;
        public void Initialize(IUnitOfWorkOptions options)
        {
            
        }

        public void AddDomainEvent(INotification domainEvent)
        {
            
        }

        public IReadOnlyCollection<INotification> GetDomainEvents()
        {
            return [];
        }

        public void ClearDomainEvents()
        {
            
        }

        // Transaction Management - Do Nothing
        public void BeginTransaction() { }
        public void Commit() { }
        public void Rollback() { }

        // DB Context - Return Default or Null
        public TDbContext1 GetOrCreateDbContext<TDbContext1>() where TDbContext1 : IPayehDbContext => default!;
        public IPayehDbContext GetDbContext() => default!;

        // Nested Unit of Work - Return Another NullUnitOfWork

        // Dispose - Do Nothing but Trigger Event
        public void Dispose() => Disposed?.Invoke(this, EventArgs.Empty);
    }
}
