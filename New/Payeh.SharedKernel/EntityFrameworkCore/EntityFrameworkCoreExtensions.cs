using Microsoft.EntityFrameworkCore;
using System.Linq;
using Payeh.SharedKernel.EntityFrameworkCore.Domain;
using Payeh.SharedKernel.EntityFrameworkCore.UnitofWork;
using Payeh.SharedKernel.UnitOfWork;

namespace Payeh.SharedKernel.EntityFrameworkCore;

/// <summary>
/// Provides extension methods to register Unit of Work and repositories in the service container.
/// </summary>
public static class EntityFrameworkCoreExtensions
{
    /// <summary>
    /// Registers the Unit of Work and repositories with the specified DbContext in the service container.
    /// </summary>
    /// <typeparam name="TContext">The type of the DbContext.</typeparam>
    /// <param name="services">The service collection.</param>
    /// <param name="builder">The action to configure the DbContext options.</param>
    /// <returns>The updated service collection.</returns>
    public static IServiceCollection AddPayehDbContext<TContext>(this IServiceCollection services, Action<DbContextOptionsBuilder>? builder) 
        where TContext : DbContext, IPayehDbContext
    {
        // Register the DbContext with the specified options
        services.AddDbContext<TContext>(builder, ServiceLifetime.Transient);
        services.AddTransient(typeof(IEfCoreDbContextProvider<>), typeof(EfCoreDbContextProvider<>));
        // Automatically register all interfaces implemented by TContext except IPayehDbContext
        var interfacesToRegister = typeof(TContext)
            .GetInterfaces()
            .Where(i => typeof(IPayehDbContext).IsAssignableFrom(i) && i != typeof(IPayehDbContext));

        foreach (var @interface in interfacesToRegister)
        {
            services.AddTransient(@interface, sp => sp.GetRequiredService<TContext>());
        }
        
        return services;
    }

}
