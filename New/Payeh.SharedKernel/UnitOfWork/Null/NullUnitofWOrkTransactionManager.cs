namespace Payeh.SharedKernel.UnitOfWork.Null;

public class NullUnitofWOrkTransactionManager : IUnitofWOrkTransactionManager
{
    public static readonly NullUnitofWOrkTransactionManager? Instance = new NullUnitofWOrkTransactionManager();
    private NullUnitofWOrkTransactionManager() { }

    public void BeginTransaction() { }

    public Task CommitTransactionAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }

    public Task RollbackTransactionAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
    public void Dispose()
    {
        
    }
}