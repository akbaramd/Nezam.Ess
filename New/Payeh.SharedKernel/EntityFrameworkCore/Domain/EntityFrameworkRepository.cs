using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Payeh.SharedKernel.Domain;
using Payeh.SharedKernel.Domain.Repositories;
using Payeh.SharedKernel.EntityFrameworkCore.UnitofWork;

namespace Payeh.SharedKernel.EntityFrameworkCore.Domain;

/// <summary>
/// Provides a base implementation of a repository for Entity Framework Core.
/// </summary>
/// <typeparam name="TEntity">The type of the entity.</typeparam>
/// <typeparam name="TDbContext">The type of the DbContext.</typeparam>
public abstract class EntityFrameworkRepository<TEntity, TDbContext> : IRepository<TEntity>
    where TEntity : Entity
    where TDbContext : IPayehDbContext
{
    internal  IEfCoreDbContextProvider<TDbContext> ContextProvider { get; }

    protected EntityFrameworkRepository(IEfCoreDbContextProvider<TDbContext> contextProvider)
    {
        ContextProvider = contextProvider ?? throw new ArgumentNullException(nameof(contextProvider));
    }

    /// <summary>
    /// Gets the current DbContext instance.
    /// </summary>
    protected async Task<TDbContext> GetDbContextAsync() => await ContextProvider.GetDbContextAsync();

    /// <summary>
    /// Gets the DbSet for the entity.
    /// </summary>
    private async Task<DbSet<TEntity>> GetDbSetAsync()
    {
        var context = await GetDbContextAsync();
        return context.Set<TEntity>();
    }

    public async Task<IQueryable<TEntity>> GetQueryableAsync()
    {
        var dbSet = await GetDbSetAsync();
        return dbSet.AsQueryable();
    }

    public async Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate)
    {
        var dbSet = await GetDbSetAsync();
        return await dbSet.Where(predicate).ToListAsync();
    }

    public async Task<TEntity?> FindOneAsync(Expression<Func<TEntity, bool>> predicate)
    {
        var dbSet = await GetDbSetAsync();
        return await PrepareQuery(dbSet).FirstOrDefaultAsync(predicate);
    }

    public async Task<TEntity> GetOneAsync(Expression<Func<TEntity, bool>> predicate)
    {
        var entity = await FindOneAsync(predicate);
        if (entity == null)
        {
            throw new InvalidOperationException("Entity not found.");
        }

        return entity;
    }

    public async Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate)
    {
        var dbSet = await GetDbSetAsync();
        return await PrepareQuery(dbSet).CountAsync(predicate);
    }

    public async Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> predicate)
    {
        var dbSet = await GetDbSetAsync();
        return await PrepareQuery(dbSet).AnyAsync(predicate);
    }

    public async Task<TEntity> AddAsync(TEntity entity, bool autoSave = false)
    {
        var dbSet = await GetDbSetAsync();
        await dbSet.AddAsync(entity);

        if (autoSave)
        {
            var context = await GetDbContextAsync();
            await context.SaveChangesAsync();
        }

        return entity;
    }

    public async Task UpdateAsync(TEntity entity, bool autoSave = false)
    {
        var dbSet = await GetDbSetAsync();
        dbSet.Update(entity);

        if (autoSave)
        {
            var context = await GetDbContextAsync();
            await context.SaveChangesAsync();
        }
    }

    public async Task DeleteAsync(TEntity entity, bool autoSave = false)
    {
        var dbSet = await GetDbSetAsync();
        dbSet.Remove(entity);

        if (autoSave)
        {
            var context = await GetDbContextAsync();
            await context.SaveChangesAsync();
        }
    }

    public async Task AddRangeAsync(IEnumerable<TEntity> entities, bool autoSave = false)
    {
        var dbSet = await GetDbSetAsync();
        await dbSet.AddRangeAsync(entities);

        if (autoSave)
        {
            var context = await GetDbContextAsync();
            await context.SaveChangesAsync();
        }
    }

    public async Task UpdateRangeAsync(IEnumerable<TEntity> entities, bool autoSave = false)
    {
        var dbSet = await GetDbSetAsync();
        dbSet.UpdateRange(entities);

        if (autoSave)
        {
            var context = await GetDbContextAsync();
            await context.SaveChangesAsync();
        }
    }

    public async Task DeleteRangeAsync(IEnumerable<TEntity> entities, bool autoSave = false)
    {
        var dbSet = await GetDbSetAsync();
        dbSet.RemoveRange(entities);

        if (autoSave)
        {
            var context = await GetDbContextAsync();
            await context.SaveChangesAsync();
        }
    }

    /// <summary>
    /// Prepares a query for the DbSet. Can be overridden to include navigation properties or filters.
    /// </summary>
    /// <param name="dbSet">The DbSet for the entity.</param>
    /// <returns>The prepared query.</returns>
    protected virtual IQueryable<TEntity> PrepareQuery(DbSet<TEntity> dbSet)
    {
        return dbSet;
    }
}
