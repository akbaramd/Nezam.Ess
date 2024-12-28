namespace Payeh.SharedKernel.UnitOfWork;

public interface IUnitofWOrkTransactionManager
{
    
    void CommitTransaction();
    void RollbackTransaction();
}