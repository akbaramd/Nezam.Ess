using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Payeh.SharedKernel.Domain;
using Payeh.SharedKernel.Domain.Repositories;
using Payeh.SharedKernel.EntityFrameworkCore.UnitofWork;
using Payeh.SharedKernel.EntityFrameworkCore.UnitOfWork;

namespace Payeh.SharedKernel.EntityFrameworkCore.Domain;

/// <summary>
/// Provides a base implementation of a read-only repository for Entity Framework Core.
/// </summary>
/// <typeparam name="TEntity">The type of the entity.</typeparam>
/// <typeparam name="TDbContext">The type of the DbContext.</typeparam>
public abstract class EfCoreReadOnlyRepository<TEntity, TDbContext> : IReadOnlyRepository<TEntity>
    where TEntity : Entity
    where TDbContext :  IPayehDbContext
{
    private readonly IEfCoreDbContextProvider<TDbContext> _databaseProvider;

    /// <summary>
    /// Initializes a new instance of the <see cref="EfCoreReadOnlyRepository{TEntity, TDbContext}"/> class.
    /// </summary>
    /// <param name="databaseProvider">The database provider instance.</param>
    protected EfCoreReadOnlyRepository(IEfCoreDbContextProvider<TDbContext> databaseProvider)
    {
        _databaseProvider = databaseProvider ?? throw new ArgumentNullException(nameof(databaseProvider));
    }

    /// <summary>
    /// Retrieves the DbContext instance, indicating read-only mode.
    /// </summary>
    /// <returns>The DbContext instance in read-only mode.</returns>
    protected async Task<TDbContext> GetDbContextAsync()
    {
        return await _databaseProvider.GetDbContextAsync(isReadOnly: true);
    }

    /// <summary>
    /// Gets the DbSet for the entity.
    /// </summary>
    protected async Task<DbSet<TEntity>> GetDbSetAsync()
    {
        var context = await GetDbContextAsync();
        return context.Set<TEntity>();
    }

    /// <summary>
    /// Retrieves an IQueryable for the entity with AsNoTracking enabled.
    /// </summary>
    /// <returns>An IQueryable for the entity.</returns>
    public async Task<IQueryable<TEntity>> GetQueryableAsync()
    {
        var dbSet = await GetDbSetAsync();
        return dbSet.AsNoTracking().AsQueryable();
    }

    /// <summary>
    /// Finds entities matching the specified predicate.
    /// </summary>
    public async Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate)
    {
        var dbSet = await GetDbSetAsync();
        return await dbSet.AsNoTracking().Where(predicate).ToListAsync();
    }

    /// <summary>
    /// Finds a single entity matching the specified predicate.
    /// </summary>
    public async Task<TEntity?> FindOneAsync(Expression<Func<TEntity, bool>> predicate)
    {
        var dbSet = await GetDbSetAsync();
        return await PrepareQuery(dbSet).FirstOrDefaultAsync(predicate);
    }

    /// <summary>
    /// Gets a single entity matching the specified predicate.
    /// Throws an exception if the entity is not found.
    /// </summary>
    public async Task<TEntity> GetOneAsync(Expression<Func<TEntity, bool>> predicate)
    {
        var entity = await FindOneAsync(predicate);
        if (entity == null)
        {
            throw new InvalidOperationException("Entity not found.");
        }

        return entity;
    }

    /// <summary>
    /// Counts the number of entities matching the specified predicate.
    /// </summary>
    public async Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate)
    {
        var dbSet = await GetDbSetAsync();
        return await PrepareQuery(dbSet).CountAsync(predicate);
    }

    /// <summary>
    /// Checks if any entity exists matching the specified predicate.
    /// </summary>
    public async Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> predicate)
    {
        var dbSet = await GetDbSetAsync();
        return await PrepareQuery(dbSet).AnyAsync(predicate);
    }

    /// <summary>
    /// Prepares a query for the DbSet. Can be overridden to include navigation properties or filters.
    /// </summary>
    protected virtual IQueryable<TEntity> PrepareQuery(DbSet<TEntity> dbSet)
    {
        return dbSet.AsNoTracking();
    }
}
