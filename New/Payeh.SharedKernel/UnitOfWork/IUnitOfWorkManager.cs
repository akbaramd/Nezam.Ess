namespace Payeh.SharedKernel.UnitOfWork
{
    public interface IUnitOfWorkManager : IDisposable
    {
        IUnitOfWork CurrentUnitOfWork { get; }
        public IUnitOfWork Begin(IUnitOfWorkOptions? options = null);
    }
}