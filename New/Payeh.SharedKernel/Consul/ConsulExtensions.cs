using Consul;

namespace Payeh.SharedKernel.Consul;

public static class ConsulServiceExtensions
{
    // Extension method to add Consul registration background service
    public static IServiceCollection AddConsulServiceRegistration(
        this IServiceCollection services,
        string serviceId,
        string serviceName,
        string host ,    // میزبان (Host) پیش‌فرض
        int port )             // پورت پیش‌فرض
    {
        
        // افزودن سرویس Consul Registration
        services.AddSingleton<IHostedService>(serviceProvider =>
            new ConsulRegistrationBackgroundService(
                serviceProvider.GetRequiredService<IConsulClient>(), // سرویس ConsulClient
                serviceProvider.GetRequiredService<ILogger<ConsulRegistrationBackgroundService>>() ,
                serviceId,
                serviceName,
                host,
                port
            ));

        return services;
    }

    // Extension method to configure Consul client
    public static IServiceCollection AddConsulClient(
        this IServiceCollection services,
        Action<ConsulClientConfiguration> consulClientConfiguration)
    {
        services.AddSingleton<IConsulService, ConsulService>(); 
        services.AddSingleton<IConsulClient>(provider =>
        {
            var config = new ConsulClientConfiguration();
            consulClientConfiguration(config);
            return new ConsulClient(config);
        });

        return services;
    }
}