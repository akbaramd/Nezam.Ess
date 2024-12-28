using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Payeh.SharedKernel.EntityFrameworkCore;

public interface IPayehDbContext : IDisposable
{
    DbSet<T> Set<T>() where T : class;
    EntityEntry<T> Entry<T>(T entity) where T : class;
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default);
    int SaveChanges();
    int SaveChanges(bool acceptAllChangesOnSuccess);
    DatabaseFacade Database { get; }
    ChangeTracker ChangeTracker { get; }
}