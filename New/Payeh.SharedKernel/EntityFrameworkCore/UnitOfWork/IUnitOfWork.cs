using Microsoft.EntityFrameworkCore;

namespace Payeh.SharedKernel.EntityFrameworkCore.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        void BeginTransaction();
        void Commit();
        Task CommitAsync();
        void Rollback();
        DbContext GetDbContext();
        Task RollbackAsync();
        IUnitOfWork CreateNestedUnitOfWork();
    }
}