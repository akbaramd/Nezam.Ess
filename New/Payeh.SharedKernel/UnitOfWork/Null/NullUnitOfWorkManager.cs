using Payeh.SharedKernel.UnitOfWork.Null;

namespace Payeh.SharedKernel.UnitOfWork;

/// <summary>
///     A null implementation of the Unit of Work Manager that returns a NullUnitOfWork.
///     Used when Unit of Work functionality is disabled or not required.
/// </summary>
public class NullUnitOfWorkManager : IUnitOfWorkManager
{
    /// <summary>
    ///     Gets the current UnitOfWork. Always throws an exception because NullUnitOfWorkManager does not manage UnitOfWork.
    /// </summary>
    public IUnitOfWork CurrentUnitOfWork => new NullUnitOfWork();

    public IUnitOfWork Begin(IUnitOfWorkOptions? options = null)
    {
        return new NullUnitOfWork();
    }



    public void Dispose()
    {
        
    }
}