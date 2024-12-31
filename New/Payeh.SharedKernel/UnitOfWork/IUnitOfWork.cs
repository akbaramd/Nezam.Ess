using MediatR;
using Payeh.SharedKernel.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Payeh.SharedKernel.UnitOfWork
{
    /// <summary>
    /// Represents the Unit of Work pattern, encapsulating transaction and data storage management.
    /// Includes support for domain events.
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        // Core Properties
        Guid Id { get; }
        IUnitOfWorkOptions Options { get; }
        event EventHandler? Disposed;

        // Transaction Management
        void AddTransaction(string key, IUnitofWOrkTransactionManager unitofWOrkTransactionManager);
        IUnitofWOrkTransactionManager? GetTransaction(string key);
        IEnumerable<string> GetTransactionKeys();
        Task CommitTransactionAsync(string key, CancellationToken cancellationToken = default);
        Task RollbackTransactionAsync(string key, CancellationToken cancellationToken = default);

        // Database/Storage Management
        void AddDataStorage(string key, IUnitOfWOrtDatabaseManager unitOfWOrtDatabaseManager);
        IUnitOfWOrtDatabaseManager? GetDataStorage(string key);
        IEnumerable<string> GetDataStorageKeys();

        // Global Operations

        Task CommitAsync(CancellationToken cancellationToken = default);
      
        Task RollbackAsync(CancellationToken cancellationToken = default);

        // Domain Events Management
        /// <summary>
        /// Adds a domain event to be published upon committing the Unit of Work.
        /// </summary>
        /// <param name="domainEvent">The domain event to add.</param>
        void AddDomainEvent(INotification domainEvent);

        void Initialize(IUnitOfWorkOptions options);
    }

    // Supporting Interfaces for Transaction and Data Storage Managers
    // (Assumed to be defined elsewhere)
}
