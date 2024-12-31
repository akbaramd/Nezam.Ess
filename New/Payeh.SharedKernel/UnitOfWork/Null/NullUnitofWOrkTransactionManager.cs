namespace Payeh.SharedKernel.UnitOfWork.Null;

public class NullUnitofWOrkTransactionManager : IUnitofWOrkTransactionManager
{
    public static readonly NullUnitofWOrkTransactionManager? Instance = new NullUnitofWOrkTransactionManager();
    private NullUnitofWOrkTransactionManager() { }

    public void BeginTransaction() { }

    public Task CommitTransactionAsync()
    {
        return Task.CompletedTask;
    }

    public Task RollbackTransactionAsync()
    {
        return Task.CompletedTask;
    }
    public void Dispose()
    {
        
    }
}