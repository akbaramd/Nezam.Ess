using Microsoft.EntityFrameworkCore.Storage;
using Payeh.SharedKernel.UnitOfWork;

namespace Payeh.SharedKernel.EntityFrameworkCore.UnitofWork;

public class EfCoreUnitofWOrkTransactionManager : IUnitofWOrkTransactionManager
{
    private readonly IPayehDbContext _dbContext;
    private readonly IDbContextTransaction? _transaction;

    /// <summary>
    /// Initializes a new instance of <see cref="EfCoreUnitofWOrkTransactionManager"/>.
    /// </summary>
    /// <param name="dbContext">The DbContext for which the transaction is created.</param>
    /// <param name="transaction">The transaction instance.</param>
    public EfCoreUnitofWOrkTransactionManager(IPayehDbContext dbContext, IDbContextTransaction? transaction)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        _transaction = transaction;
    }

    /// <summary>
    /// Commits the current transaction.
    /// </summary>
    public void CommitTransaction()
    {
        try
        {
            _transaction?.Commit();
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("Failed to commit the transaction.", ex);
        }
    }

    /// <summary>
    /// Rolls back the current transaction.
    /// </summary>
    public void RollbackTransaction()
    {
        try
        {
            _transaction?.Rollback();
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("Failed to rollback the transaction.", ex);
        }
    }

    /// <summary>
    /// Disposes the transaction instance.
    /// </summary>
    public void Dispose()
    {
        _transaction?.Dispose();
    }
}