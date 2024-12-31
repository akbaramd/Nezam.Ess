namespace Payeh.SharedKernel.UnitOfWork;

public interface IUnitofWOrkTransactionManager
{
    
    Task CommitTransactionAsync();
    Task RollbackTransactionAsync();
    void Dispose();
}