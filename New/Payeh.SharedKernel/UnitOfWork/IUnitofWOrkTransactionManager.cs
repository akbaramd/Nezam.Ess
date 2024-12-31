namespace Payeh.SharedKernel.UnitOfWork;

public interface IUnitofWOrkTransactionManager
{
    
    Task CommitTransactionAsync(CancellationToken cancellationToken);
    Task RollbackTransactionAsync(CancellationToken cancellationToken);
    void Dispose();
}