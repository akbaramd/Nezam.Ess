using Microsoft.EntityFrameworkCore;
using Payeh.SharedKernel.EntityFrameworkCore.UnitOfWork;

namespace Payeh.SharedKernel.EntityFrameworkCore;

/// <summary>
/// Provides extension methods to register Unit of Work and repositories in the service container.
/// </summary>
public static class UnitOfWorkExtensions
{
    /// <summary>
    /// Registers the Unit of Work and repositories with the specified DbContext in the service container.
    /// </summary>
    /// <typeparam name="TContext">The type of the DbContext.</typeparam>
    /// <param name="services">The service collection.</param>
    /// <returns>The updated service collection.</returns>
    public static IServiceCollection AddPayehDbContext<TContext>(this IServiceCollection services,Action<DbContextOptionsBuilder>? builder) where TContext : DbContext
    {
        services.AddDbContext<TContext>(builder,ServiceLifetime.Transient);
        services.AddPayehUnitOfWork<TContext>();

        return services;
    }
    
    public static IServiceCollection AddPayehUnitOfWork<TContext>(this IServiceCollection services) where TContext : DbContext
    {
        services.AddSingleton<IUnitOfWorkManager, UnitOfWorkManager<TContext>>();

        return services;
    }
    
}