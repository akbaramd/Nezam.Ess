using Microsoft.EntityFrameworkCore;
using Payeh.SharedKernel.Domain;
using Payeh.SharedKernel.Domain.Repositories;

namespace Payeh.SharedKernel.EntityFrameworkCore.Domain;

/// <summary>
/// Provides extension methods for repositories to retrieve DbContext and DbSet instances.
/// </summary>
public static class RepositoryExtensions
{
    /// <summary>
    /// Asynchronously retrieves the DbContext associated with the specified repository.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    /// <typeparam name="TDbContext">The type of the DbContext.</typeparam>
    /// <param name="repository">The repository instance.</param>
    /// <returns>A Task resolving to the DbContext associated with the repository.</returns>
    /// <exception cref="InvalidOperationException">Thrown if the repository is not an EntityFrameworkRepository.</exception>
    public static async Task<TDbContext> GetDbContextAsync<TEntity, TDbContext>(this IRepository<TEntity> repository)
        where TEntity : Entity
        where TDbContext : IPayehDbContext
    {
        if (repository is EntityFrameworkRepository<TEntity, TDbContext> efRepository)
        {
            return await efRepository.ContextProvider.GetDbContextAsync();
        }

        throw new InvalidOperationException(
            $"The repository must derive from EntityFrameworkRepository<{typeof(TEntity).Name}, {typeof(TDbContext).Name}>. " +
            $"Received: {repository.GetType().Name}");
    }

    /// <summary>
    /// Retrieves the DbSet for the specified entity type from the repository.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    /// <typeparam name="TDbContext">The type of the DbContext.</typeparam>
    /// <param name="repository">The repository instance.</param>
    /// <returns>The DbSet associated with the entity type.</returns>
    /// <exception cref="InvalidOperationException">Thrown if the repository is not an EntityFrameworkRepository.</exception>
    public static async Task<DbSet<TEntity>> GetDbSetAsync<TEntity, TDbContext>(this IRepository<TEntity> repository)
        where TEntity : Entity
        where TDbContext : IPayehDbContext
    {
        var dbContext = await repository.GetDbContextAsync<TEntity, TDbContext>();
        return dbContext.Set<TEntity>();
    }

    /// <summary>
    /// Retrieves a key associated with the DbContext for advanced use cases (e.g., multi-tenancy).
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    /// <typeparam name="TDbContext">The type of the DbContext.</typeparam>
    /// <param name="repository">The repository instance.</param>
    /// <returns>A string representing the unique key associated with the DbContext.</returns>
    /// <exception cref="InvalidOperationException">Thrown if the repository is not an EntityFrameworkRepository.</exception>
    public static string GetDbContextKey<TEntity, TDbContext>(this IRepository<TEntity> repository)
        where TEntity : Entity
        where TDbContext : IPayehDbContext
    {
        if (repository is EntityFrameworkRepository<TEntity, TDbContext>)
        {
            return typeof(TDbContext).FullName ?? throw new InvalidOperationException("DbContext type name is null.");
        }

        throw new InvalidOperationException(
            $"The repository must derive from EntityFrameworkRepository<{typeof(TEntity).Name}, {typeof(TDbContext).Name}>. " +
            $"Received: {repository.GetType().Name}");
    }
}
