using Payeh.SharedKernel.UnitOfWork.Null;

namespace Payeh.SharedKernel.UnitOfWork;

/// <summary>
/// Provides extension methods to register Unit of Work and repositories in the service container.
/// </summary>
public static class UnitOfWorkExtensions
{
    /// <summary>
    /// Registers the Unit of Work manager with the specified DbContext in the service container.
    /// </summary>
    /// <typeparam name="TContext">The type of the DbContext.</typeparam>
    /// <param name="services">The service collection.</param>
    /// <returns>The updated service collection.</returns>
    public static IServiceCollection AddPayehUnitOfWork(this IServiceCollection services,
        Action<UnitOfWorkOptions>? configureOptions = null)
    {
        var options = new UnitOfWorkOptions();
        configureOptions?.Invoke(options);

        services.AddSingleton<IUnitOfWorkOptions>(options);

        if (options.IsUnitOfWorkEnabled)
        {
            services.AddSingleton<IUnitOfWorkManager, UnitOfWorkManager>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();
        }
        else
        {
            services.AddSingleton<IUnitOfWorkManager, NullUnitOfWorkManager>();
            services.AddTransient<IUnitOfWork, NullUnitOfWork>();
        }

        return services;
    }
}