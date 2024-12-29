using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Abstractions;
using Payeh.SharedKernel.UnitOfWork;
using Payeh.SharedKernel.UnitOfWork.Null;

namespace Payeh.SharedKernel.EntityFrameworkCore.UnitofWork;

/// <summary>
///     Provides a DbContext instance tied to the current Unit of Work.
///     Ensures transactional integrity and avoids redundant DbContext creation.
///     Falls back to creating standalone DbContext if Unit of Work is not registered.
/// </summary>
/// <typeparam name="TDbContext">The type of the DbContext.</typeparam>
public class EfCoreDbContextProvider<TDbContext> : IEfCoreDbContextProvider<TDbContext>
    where TDbContext : IPayehDbContext
{
    private const string TransactionsNotSupportedWarningMessage =
        "The current database does not support transactions. Database consistency may not be guaranteed.";

    private readonly IServiceProvider _serviceProvider;
    private readonly IUnitOfWorkManager? _unitOfWorkManager; // Nullable to allow fallback

    public EfCoreDbContextProvider(
        IServiceProvider serviceProvider,
        IUnitOfWorkManager? unitOfWorkManager) // UnitOfWorkManager can be null
    {
        _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
        _unitOfWorkManager = unitOfWorkManager; // Nullable for fallback behavior
        Logger = NullLogger<EfCoreDbContextProvider<TDbContext>>.Instance;
    }

    public ILogger<EfCoreDbContextProvider<TDbContext>> Logger { get; set; }

    /// <summary>
    ///     Retrieves or creates a DbContext instance and optionally initiates a transaction.
    /// </summary>
    /// <param name="createTransaction">Indicates whether a transaction should be created.</param>
    /// <param name="cancellationToken">Cancellation token for async operations.</param>
    /// <returns>A Task resolving to the requested DbContext.</returns>
    public async Task<TDbContext> GetDbContextAsync(bool isReadOnly = true,
        CancellationToken cancellationToken = default)
    {
        // Handle case where UnitOfWorkManager is null (fallback mode)
        if (isReadOnly || _unitOfWorkManager is null || _unitOfWorkManager is NullUnitOfWorkManager)
        {
            Logger.LogWarning("UnitOfWorkManager is not registered. Creating standalone DbContext.");
            return _serviceProvider.GetRequiredService<TDbContext>();
        }

        // Ensure there's an active Unit of Work
        var unitOfWork = _unitOfWorkManager.CurrentUnitOfWork;
        if (unitOfWork is null)
            throw new InvalidOperationException("DbContext must be used inside an active Unit of Work.");

        // Check if the DbContext is already registered in the Unit of Work
        var dbContextKey = typeof(TDbContext).FullName
                           ?? throw new InvalidOperationException("The type name for the DbContext is null.");

        if (unitOfWork.GetDataStorage(dbContextKey) is EfCoreUnitOfWOrtDatabaseManager existingStorage)
            return (TDbContext)existingStorage.DbContext;

        // Create and register a new DbContext in Unit of Work
        var dbContext = _serviceProvider.GetRequiredService<TDbContext>();
        unitOfWork.AddDataStorage(dbContextKey, new EfCoreUnitOfWOrtDatabaseManager(dbContext));

        if (unitOfWork.Options.IsTransactional) await CreateTransactionAsync(unitOfWork, dbContext, cancellationToken);

        return dbContext;
    }

    /// <summary>
    ///     Initiates a transaction for the provided DbContext, if not already present.
    /// </summary>
    /// <param name="unitOfWork">The current Unit of Work.</param>
    /// <param name="dbContext">The DbContext to attach the transaction to.</param>
    /// <param name="cancellationToken">Cancellation token for async operations.</param>
    private async Task CreateTransactionAsync(IUnitOfWork unitOfWork, TDbContext dbContext,
        CancellationToken cancellationToken)
    {
        var transactionKey = $"EntityFrameworkCore_{typeof(TDbContext).Name}";

        // Skip if a transaction is already registered for this DbContext
        if (unitOfWork.GetTransaction(transactionKey) is not null) return;

        try
        {
            var dbTransaction =
                await dbContext.Database.BeginTransactionAsync(unitOfWork.Options.IsolationLevel, cancellationToken);
            unitOfWork.AddTransaction(transactionKey, new EfCoreUnitofWOrkTransactionManager(dbContext, dbTransaction));
        }
        catch (InvalidOperationException ex)
        {
            Logger.LogWarning(TransactionsNotSupportedWarningMessage, ex);
        }
        catch (NotSupportedException ex)
        {
            Logger.LogWarning(TransactionsNotSupportedWarningMessage, ex);
        }
    }
}