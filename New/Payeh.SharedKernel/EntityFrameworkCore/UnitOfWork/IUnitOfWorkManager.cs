namespace Payeh.SharedKernel.EntityFrameworkCore.UnitOfWork
{
    public interface IUnitOfWorkManager : IDisposable
    {
        IUnitOfWork CurrentUnitOfWork { get; }
        public IUnitOfWork Begin(bool startTransaction = true);
    }
}