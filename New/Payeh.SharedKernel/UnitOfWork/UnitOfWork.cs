using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace Payeh.SharedKernel.UnitOfWork
{
    /// <summary>
    /// Implements the Unit of Work pattern, managing transactions, data storage, and domain events.
    /// </summary>
    public class UnitOfWork : IUnitOfWork
    {
        // Thread-safe collections for transactions and data storages
        private readonly ConcurrentDictionary<string, IUnitofWOrkTransactionManager> _transactions = new();
        private readonly ConcurrentDictionary<string, IUnitOfWOrtDatabaseManager> _dataStorages = new();

        // Mediator for publishing domain events
        private readonly IMediator _mediator;

        // Disposal flag
        private bool _isDisposed;

        // Local domain events collection
        private readonly List<INotification> _localDomainEvents = new();

        /// <summary>
        /// Unique identifier for the Unit of Work instance.
        /// </summary>
        public Guid Id { get; } = Guid.NewGuid();

        /// <summary>
        /// Options configuring the Unit of Work behavior.
        /// </summary>
        public IUnitOfWorkOptions Options { get; private set; }

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

        public void AddTransaction(string key, IUnitofWOrkTransactionManager transactionManager)
        {
            if (!_transactions.TryAdd(key, transactionManager))
                throw new InvalidOperationException($"Transaction with key '{key}' already exists.");
        }

        public IUnitofWOrkTransactionManager? GetTransaction(string key)
            => _transactions.TryGetValue(key, out var transaction) ? transaction : null;

        public IEnumerable<string> GetTransactionKeys() => _transactions.Keys;

     
        public async Task CommitTransactionAsync(string key, CancellationToken cancellationToken = default)
        {
            var transaction = GetTransaction(key);
            if (transaction != null)
                await transaction.CommitTransactionAsync(cancellationToken);
        }

    

        public async Task RollbackTransactionAsync(string key, CancellationToken cancellationToken = default)
        {
            var transaction = GetTransaction(key);
            if (transaction != null)
                await transaction.RollbackTransactionAsync(cancellationToken);
        }

        #endregion

        #region Data Storage Management

        public void AddDataStorage(string key, IUnitOfWOrtDatabaseManager databaseManager)
        {
            if (!_dataStorages.TryAdd(key, databaseManager))
                throw new InvalidOperationException($"DataStorage with key '{key}' already exists.");
        }

        public IUnitOfWOrtDatabaseManager? GetDataStorage(string key)
            => _dataStorages.TryGetValue(key, out var dataStorage) ? dataStorage : null;

        public IEnumerable<string> GetDataStorageKeys() => _dataStorages.Keys;

        #endregion

        #region Global Commit and Rollback

       

        public async Task CommitAsync(CancellationToken cancellationToken = default)
        {
            EnsureNotDisposed();

            foreach (var transaction in _transactions.Values)
            {
                await transaction.CommitTransactionAsync(cancellationToken);
            }

            foreach (var dataStorage in _dataStorages.Values)
            {
                await dataStorage.SaveChangesAsync(cancellationToken);
            }

            await PublishDomainEventsAsync(cancellationToken);
            ClearDomainEvents();
        }



        public async Task RollbackAsync(CancellationToken cancellationToken = default)
        {
            EnsureNotDisposed();

            foreach (var transaction in _transactions.Values)
            {
                await transaction.RollbackTransactionAsync(cancellationToken);
            }
        }

      

        #endregion

        #region Domain Events Management

        public void AddDomainEvent(INotification domainEvent)
        {
            if (domainEvent == null)
                throw new ArgumentNullException(nameof(domainEvent));

            _localDomainEvents.Add(domainEvent);
        }

        public void Initialize(IUnitOfWorkOptions options)
        {
            Options = options;
        }

        public IReadOnlyCollection<INotification> GetDomainEvents()
            => _localDomainEvents.AsReadOnly();

        public void ClearDomainEvents()
        {
            _localDomainEvents.Clear();
        }

        private async Task PublishDomainEventsAsync(CancellationToken cancellationToken)
        {
            foreach (var domainEvent in _localDomainEvents)
            {
                await _mediator.Publish(domainEvent, cancellationToken);
            }
        }


        #endregion

        #region Disposal and Validation

        public void Dispose()
        {
            if (_isDisposed) return;

            foreach (var transaction in _transactions.Values)
            {
                transaction.Dispose();
            }

            foreach (var dataStorage in _dataStorages.Values)
            {
                dataStorage.Dispose();
            }

            _transactions.Clear();
            _dataStorages.Clear();
            _isDisposed = true;

            Disposed?.Invoke(this, EventArgs.Empty);
        }

        private void EnsureNotDisposed()
        {
            if (_isDisposed)
                throw new ObjectDisposedException(nameof(UnitOfWork));
        }

        #endregion
    }
}