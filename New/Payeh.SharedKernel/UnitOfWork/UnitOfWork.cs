using MediatR;

namespace Payeh.SharedKernel.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly Dictionary<string, IUnitofWOrkTransactionManager> _transactions = new();
        private readonly Dictionary<string, IUnitOfWOrtDatabaseManager> _dataStorages = new();
        private readonly IMediator _mediator;
        private bool _isDisposed;

        public IUnitOfWorkOptions Options { get; set; }
        public event EventHandler? Disposed;

        public UnitOfWork(IMediator mediator, IUnitOfWorkOptions options)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            Options = options ?? throw new ArgumentNullException(nameof(options));
        }

        // Transaction Management
        public void AddTransaction(string key, IUnitofWOrkTransactionManager unitofWOrkTransactionManager)
        {
            if (_transactions.ContainsKey(key))
                throw new InvalidOperationException($"Transaction with key '{key}' already exists.");

            _transactions[key] = unitofWOrkTransactionManager;
        }

        public IUnitofWOrkTransactionManager? GetTransaction(string key) =>
            _transactions.TryGetValue(key, out var transaction) ? transaction : null;

        public IEnumerable<string> GetTransactionKeys() => _transactions.Keys;

        public void CommitTransaction(string key)
        {
            GetTransaction(key)?.CommitTransaction();
        }

        public async Task CommitTransactionAsync(string key, CancellationToken cancellationToken = default)
        {
            var transaction = GetTransaction(key);
            if (transaction != null)
                await Task.Run(transaction.CommitTransaction, cancellationToken);
        }

        public void RollbackTransaction(string key)
        {
            GetTransaction(key)?.RollbackTransaction();
        }

        public async Task RollbackTransactionAsync(string key, CancellationToken cancellationToken = default)
        {
            var transaction = GetTransaction(key);
            if (transaction != null)
                await Task.Run(transaction.RollbackTransaction, cancellationToken);
        }

        // Database/Storage Management
        public void AddDataStorage(string key, IUnitOfWOrtDatabaseManager unitOfWOrtDatabaseManager)
        {
            if (_dataStorages.ContainsKey(key))
                throw new InvalidOperationException($"DataStorage with key '{key}' already exists.");

            _dataStorages[key] = unitOfWOrtDatabaseManager;
        }

        public IUnitOfWOrtDatabaseManager? GetDataStorage(string key) =>
            _dataStorages.TryGetValue(key, out var dataStorage) ? dataStorage : null;

        public IEnumerable<string> GetDataStorageKeys() => _dataStorages.Keys;

        // Global Commit and Rollback
        public async Task CommitAsync(CancellationToken cancellationToken = default)
        {
            if (_isDisposed)
                throw new ObjectDisposedException(nameof(UnitOfWork));

            try
            {
                // Save changes for all data storages
                foreach (var dataManager in _dataStorages.Values)
                {
                    await dataManager.SaveChangesAsync(cancellationToken);
                }

                // Handle transactions if enabled
                if (Options.IsTransactional)
                {
                    foreach (var transaction in _transactions.Values)
                    {
                        transaction.CommitTransaction();
                    }
                }

                await PublishDomainEventsAsync();
                
                Dispose();
            }
            catch
            {
                await RollbackAsync(cancellationToken);
                throw;
            }
        }

        public void CommitAll()
        {
            if (_isDisposed)
                throw new ObjectDisposedException(nameof(UnitOfWork));

            if (Options.IsTransactional)
            {
                foreach (var transaction in _transactions.Values)
                {
                    transaction.CommitTransaction();
                }
                
            }
            PublishDomainEventsAsync().Wait();
        }

        public async Task RollbackAsync(CancellationToken cancellationToken = default)
        {
            if (Options.IsTransactional)
            {
                foreach (var transaction in _transactions.Values)
                {
                    await Task.Run(transaction.RollbackTransaction, cancellationToken);
                }
            }
        }

        public void Initialize(IUnitOfWorkOptions options)
        {
            Options = options;
        }

        public void RollbackAll()
        {
            if (Options.IsTransactional)
            {
                foreach (var transaction in _transactions.Values)
                {
                    transaction.RollbackTransaction();
                }
            }
        }

        private async Task PublishDomainEventsAsync()
        {
            var domainEvents = _dataStorages.Values
                .SelectMany(storage => storage.GetDomainEvents())
                .ToList();

            foreach (var domainEvent in domainEvents)
            {
                await _mediator.Publish(domainEvent);
            }

            foreach (var dataStorage in _dataStorages.Values)
            {
                dataStorage.ClearDomainEvents();
            }
        }

        public void Dispose()
        {
            if (_isDisposed) return;

            _transactions.Clear();
            _dataStorages.Clear();
            _isDisposed = true;

            Disposed?.Invoke(this, EventArgs.Empty);
        }
    }
}
