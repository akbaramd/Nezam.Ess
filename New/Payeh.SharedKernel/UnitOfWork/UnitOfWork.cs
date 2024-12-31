using MediatR;
using Payeh.SharedKernel.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Payeh.SharedKernel.UnitOfWork
{
    /// <summary>
    /// Implements the Unit of Work pattern, managing transactions, data storage, and domain events.
    /// </summary>
    public class UnitOfWork : IUnitOfWork
    {
        // Internal Dictionaries for Transactions and Data Storages
        private readonly Dictionary<string, IUnitofWOrkTransactionManager> _transactions = new();
        private readonly Dictionary<string, IUnitOfWOrtDatabaseManager> _dataStorages = new();

        // Mediator for Publishing Domain Events
        private readonly IMediator _mediator;

        // Disposal Flag
        private bool _isDisposed;

        // Local Domain Events Collection
        private readonly List<INotification> _localDomainEvents = new();

        /// <summary>
        /// Unique identifier for the Unit of Work instance.
        /// </summary>
        public Guid Id { get; } = Guid.NewGuid();

        /// <summary>
        /// Options configuring the Unit of Work behavior.
        /// </summary>
        public IUnitOfWorkOptions Options { get; set; }

        /// <summary>
        /// Event triggered upon disposal of the Unit of Work.
        /// </summary>
        public event EventHandler? Disposed;

        /// <summary>
        /// Initializes a new instance of the <see cref="UnitOfWork"/> class.
        /// </summary>
        /// <param name="mediator">The mediator for publishing domain events.</param>
        /// <param name="options">The unit of work options.</param>
        public UnitOfWork(IMediator mediator, IUnitOfWorkOptions options)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            Options = options ?? throw new ArgumentNullException(nameof(options));
        }

        #region Transaction Management

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
            var transaction = GetTransaction(key);
            if (transaction != null)
            {
                // Fire and forget the async commit
                _ = transaction.CommitTransactionAsync();
            }
        }

        public async Task CommitTransactionAsync(string key, CancellationToken cancellationToken = default)
        {
            var transaction = GetTransaction(key);
            if (transaction != null)
                await transaction.CommitTransactionAsync();
        }

        public void RollbackTransaction(string key)
        {
            var transaction = GetTransaction(key);
            if (transaction != null)
            {
                // Fire and forget the async rollback
                _ = transaction.RollbackTransactionAsync();
            }
        }

        public async Task RollbackTransactionAsync(string key, CancellationToken cancellationToken = default)
        {
            var transaction = GetTransaction(key);
            if (transaction != null)
                await transaction.RollbackTransactionAsync();
        }

        #endregion

        #region Database/Storage Management

        public void AddDataStorage(string key, IUnitOfWOrtDatabaseManager unitOfWOrtDatabaseManager)
        {
            if (_dataStorages.ContainsKey(key))
                throw new InvalidOperationException($"DataStorage with key '{key}' already exists.");

            _dataStorages[key] = unitOfWOrtDatabaseManager;
        }

        public IUnitOfWOrtDatabaseManager? GetDataStorage(string key) =>
            _dataStorages.TryGetValue(key, out var dataStorage) ? dataStorage : null;

        public IEnumerable<string> GetDataStorageKeys() => _dataStorages.Keys;

        #endregion

        #region Global Commit and Rollback

        /// <summary>
        /// Commits all changes and publishes domain events asynchronously.
        /// </summary>
        public async Task CommitAsync(CancellationToken cancellationToken = default)
        {
            if (_isDisposed)
                throw new ObjectDisposedException(nameof(UnitOfWork));

            try
            {
                await SaveChangesAsync(cancellationToken);
                await PublishDomainEventsAsync(cancellationToken);
                await CommitTransactionsAsync(cancellationToken);
                DisposeTransactions();
            }
            catch
            {
                await RollbackAsync(cancellationToken);
                throw;
            }
        }

        /// <summary>
        /// Commits all changes and publishes domain events synchronously.
        /// </summary>
        public void CommitAll()
        {
            if (_isDisposed)
                throw new ObjectDisposedException(nameof(UnitOfWork));

            try
            {
                // Synchronously save changes
                SaveChangesSync();

                // Publish domain events synchronously
                PublishDomainEventsSync();

                // Commit transactions synchronously
                CommitTransactionsSync();

                DisposeTransactions();
            }
            catch
            {
                RollbackAll();
                throw;
            }
        }

        /// <summary>
        /// Rolls back all transactions asynchronously.
        /// </summary>
        public async Task RollbackAsync(CancellationToken cancellationToken = default)
        {
            if (Options.IsTransactional)
            {
                foreach (var transaction in _transactions.Values)
                {
                    await transaction.RollbackTransactionAsync();
                }
            }
        }

        /// <summary>
        /// Rolls back all transactions synchronously.
        /// </summary>
        public void RollbackAll()
        {
            if (Options.IsTransactional)
            {
                foreach (var transaction in _transactions.Values)
                {
                    transaction.RollbackTransactionAsync().Wait();
                }
            }
        }

        /// <summary>
        /// Initializes the UnitOfWork with the given options.
        /// </summary>
        /// <param name="options">The unit of work options.</param>
        public void Initialize(IUnitOfWorkOptions options)
        {
            Options = options;
        }

        #endregion

        #region Domain Events Management

        /// <summary>
        /// Adds a domain event to be published upon committing the Unit of Work.
        /// </summary>
        /// <param name="domainEvent">The domain event to add.</param>
        public void AddDomainEvent(INotification domainEvent)
        {
            if (domainEvent == null)
                throw new ArgumentNullException(nameof(domainEvent));

            _localDomainEvents.Add(domainEvent);
        }

        /// <summary>
        /// Retrieves all domain events added to the Unit of Work.
        /// </summary>
        /// <returns>A read-only collection of domain events.</returns>
        public IReadOnlyCollection<INotification> GetDomainEvents() => _localDomainEvents.AsReadOnly();

        /// <summary>
        /// Clears all domain events from the Unit of Work.
        /// </summary>
        public void ClearDomainEvents()
        {
            _localDomainEvents.Clear();
        }

        /// <summary>
        /// Publishes both local domain events asynchronously.
        /// </summary>
        private async Task PublishDomainEventsAsync(CancellationToken cancellationToken)
        {
            var domainEvents = _localDomainEvents.ToList();

            foreach (var domainEvent in domainEvents)
            {
                await _mediator.Publish(domainEvent, cancellationToken);
            }

            // Clear local domain events after publishing
            ClearDomainEvents();
        }

        /// <summary>
        /// Publishes both local domain events synchronously.
        /// </summary>
        private void PublishDomainEventsSync()
        {
            var domainEvents = _localDomainEvents.ToList();

            foreach (var domainEvent in domainEvents)
            {
                _mediator.Publish(domainEvent).GetAwaiter().GetResult();
            }

            // Clear local domain events after publishing
            ClearDomainEvents();
        }

        #endregion

        #region Transaction Commit/Rollback

        /// <summary>
        /// Commits all transactions asynchronously.
        /// </summary>
        private async Task CommitTransactionsAsync(CancellationToken cancellationToken)
        {
            if (Options.IsTransactional)
            {
                foreach (var transaction in _transactions.Values)
                {
                    await transaction.CommitTransactionAsync();
                }
            }
        }

        /// <summary>
        /// Commits all transactions synchronously.
        /// </summary>
        private void CommitTransactionsSync()
        {
            if (Options.IsTransactional)
            {
                foreach (var transaction in _transactions.Values)
                {
                    transaction.CommitTransactionAsync().Wait();
                }
            }
        }

        #endregion

        #region Saving Changes

        /// <summary>
        /// Saves changes asynchronously for all data storages.
        /// </summary>
        /// <param name="cancellationToken">Cancellation token.</param>
        private async Task SaveChangesAsync(CancellationToken cancellationToken)
        {
            foreach (var dataManager in _dataStorages.Values)
            {
                await dataManager.SaveChangesAsync(cancellationToken);
            }
        }

        /// <summary>
        /// Saves changes synchronously for all data storages.
        /// </summary>
        private void SaveChangesSync()
        {
            foreach (var dataManager in _dataStorages.Values)
            {
                dataManager.SaveChangesAsync().GetAwaiter().GetResult();
            }
        }

        #endregion

        #region Dispose and Cleanup

        /// <summary>
        /// Disposes the UnitOfWork, releasing all resources.
        /// </summary>
        public void Dispose()
        {
            if (_isDisposed) return;

            // Dispose transactions
            DisposeTransactions();

            // Dispose data storages if necessary
            foreach (var dataStorage in _dataStorages.Values)
            {
                if (dataStorage is IDisposable disposable)
                {
                    disposable.Dispose();
                }
            }
            _dataStorages.Clear();

            _isDisposed = true;

            OnDisposed();
        }

        /// <summary>
        /// Disposes all transaction managers.
        /// </summary>
        private void DisposeTransactions()
        {
            foreach (var transaction in _transactions.Values)
            {
                transaction.Dispose();
            }

            _transactions.Clear();
        }

        /// <summary>
        /// Raises the Disposed event.
        /// </summary>
        protected virtual void OnDisposed()
        {
            Disposed?.Invoke(this, EventArgs.Empty);
        }

        #endregion
    }
}
