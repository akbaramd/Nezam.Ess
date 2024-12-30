namespace Payeh.SharedKernel.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        public Guid Id { get; set; }
        public IUnitOfWorkOptions Options { get; }   
        public event EventHandler? Disposed;
        // Transaction Management
        void AddTransaction(string key, IUnitofWOrkTransactionManager unitofWOrkTransactionManager);
        IUnitofWOrkTransactionManager? GetTransaction(string key);
        IEnumerable<string> GetTransactionKeys();
        void CommitTransaction(string key);
        Task CommitTransactionAsync(string key, CancellationToken cancellationToken = default);
        void RollbackTransaction(string key);
        Task RollbackTransactionAsync(string key, CancellationToken cancellationToken = default);

        // Database/Storage Management
        void AddDataStorage(string key, IUnitOfWOrtDatabaseManager unitOfWOrtDatabaseManager);
        IUnitOfWOrtDatabaseManager? GetDataStorage(string key);
        IEnumerable<string> GetDataStorageKeys();

        // Global Operations
        void CommitAll();
        Task CommitAsync(CancellationToken cancellationToken = default);
        void RollbackAll();
        Task RollbackAsync(CancellationToken cancellationToken = default);
        void Initialize(IUnitOfWorkOptions options);
    }

    // Supporting Interface for Transaction
}