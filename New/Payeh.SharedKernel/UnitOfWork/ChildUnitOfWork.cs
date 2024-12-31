using MediatR;

namespace Payeh.SharedKernel.UnitOfWork
{
    /// <summary>
    /// A child UnitOfWork that delegates all operations to a parent <see cref="IUnitOfWork"/>.
    /// This allows you to create nested UoW layers (logically) without duplicating 
    /// the transactional state or resources. 
    /// </summary>
    public class ChildUnitOfWork : IUnitOfWork
    {
        private readonly IUnitOfWork _parentUow;


        /// <summary>
        /// Creates a new ChildUnitOfWork that delegates everything to <paramref name="parentUow"/>.
        /// </summary>
        public ChildUnitOfWork(IUnitOfWork parentUow)
        {
            _parentUow = parentUow ?? throw new ArgumentNullException(nameof(parentUow));
            _parentUow.Disposed += (sender, args) =>
            {
                Disposed?.Invoke(sender!, args);
            };
        }

        /// <summary>
        /// In most child implementations, we use the parent's Id 
        /// since they share the same underlying transaction/logical scope.
        /// </summary>
        public Guid Id => _parentUow.Id;

        /// <summary>
        /// Returns the parent's options, effectively inheriting them.
        /// </summary>
        public IUnitOfWorkOptions Options => _parentUow.Options;

        /// <summary>
        /// Event that is typically raised on disposal, 
        /// but we forward it to the parent so consumers can subscribe in a single place.
        /// </summary>
        public event EventHandler Disposed = default!;

        #region Transaction Management

        public void AddTransaction(string key, IUnitofWOrkTransactionManager unitofWOrkTransactionManager)
            => _parentUow.AddTransaction(key, unitofWOrkTransactionManager);

        public IUnitofWOrkTransactionManager? GetTransaction(string key)
            => _parentUow.GetTransaction(key);

        public IEnumerable<string> GetTransactionKeys()
            => _parentUow.GetTransactionKeys();

    


        public Task CommitTransactionAsync(string key, CancellationToken cancellationToken = default)
            => _parentUow.CommitTransactionAsync(key, cancellationToken);



        public Task RollbackTransactionAsync(string key, CancellationToken cancellationToken = default)
            => _parentUow.RollbackTransactionAsync(key, cancellationToken);

        #endregion

        #region Data Storage Management

        public void AddDataStorage(string key, IUnitOfWOrtDatabaseManager unitOfWOrtDatabaseManager)
            => _parentUow.AddDataStorage(key, unitOfWOrtDatabaseManager);

        public IUnitOfWOrtDatabaseManager? GetDataStorage(string key)
            => _parentUow.GetDataStorage(key);

        public IEnumerable<string> GetDataStorageKeys()
            => _parentUow.GetDataStorageKeys();

        #endregion

        #region Global Operations

        /// <summary>
        /// Typically, a child does not finalize (commit) the entire UoW. 
        /// However, you can choose to delegate it to the parent if desired.
        /// </summary>
       

        public Task CommitAsync(CancellationToken cancellationToken = default)
        {
            return Task.CompletedTask;
        }
   

        public Task RollbackAsync(CancellationToken cancellationToken = default)
            => _parentUow.RollbackAsync(cancellationToken);

  

        public void AddDomainEvent(INotification domainEvent)
        {
            _parentUow.AddDomainEvent(domainEvent);
        }

        public void Initialize(IUnitOfWorkOptions options)
        {
            _parentUow.Initialize(options);
        }

        #endregion

        #region IDisposable

        /// <summary>
        /// Disposing the child typically does NOT dispose the parent, 
        /// so here you can either do nothing or just remove event handlers, etc.
        /// </summary>
        public void Dispose()
        {
          
           
        }

        #endregion

        /// <summary>
        /// For diagnostics/logging.
        /// </summary>
        public override string ToString()
            => $"[ChildUnitOfWork => ParentId: {_parentUow.Id}]";
    }
}
