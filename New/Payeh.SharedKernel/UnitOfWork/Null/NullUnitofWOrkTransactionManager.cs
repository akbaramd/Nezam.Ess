namespace Payeh.SharedKernel.UnitOfWork.Null;

public class NullUnitofWOrkTransactionManager : IUnitofWOrkTransactionManager
{
    public static readonly NullUnitofWOrkTransactionManager? Instance = new NullUnitofWOrkTransactionManager();
    private NullUnitofWOrkTransactionManager() { }

    public void BeginTransaction() { }
    public void CommitTransaction() { }
    public void RollbackTransaction() { }
}