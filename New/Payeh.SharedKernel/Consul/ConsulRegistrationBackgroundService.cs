using Consul;

namespace Payeh.SharedKernel.Consul;

public class ConsulRegistrationBackgroundService : BackgroundService
{
    private readonly IConsulClient _consulClient;
    private readonly ILogger<ConsulRegistrationBackgroundService> _logger;
    private readonly string _serviceId;
    private readonly string _serviceName;
    private readonly string _host;
    private readonly int _port;
    private readonly string _healthCheckUrl;

    public ConsulRegistrationBackgroundService(
        IConsulClient consulClient,
        ILogger<ConsulRegistrationBackgroundService> logger,
        string serviceId,
        string serviceName,
        string host,
        int port,
        string healthCheckUrl = "/health")  // Default health check endpoint
    {
        _consulClient = consulClient;
        _logger = logger;
        _serviceId = serviceId;
        _serviceName = serviceName;
        _host = host;
        _port = port;
        _healthCheckUrl = healthCheckUrl;  // Assign health check URL
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        try
        {
            // Register the service with Consul along with health check
            var registration = new AgentServiceRegistration
            {
                ID = _serviceId,
                Name = _serviceName,
                Address = _host,
                Port = _port,
            };

            _logger.LogInformation("Registering service {ServiceName} with ID {ServiceId} at {Host}:{Port}", _serviceName, _serviceId, _host, _port);
            await _consulClient.Agent.ServiceRegister(registration, stoppingToken);

            await Task.Delay(Timeout.Infinite, stoppingToken); // Keep the service registered until cancellation
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while registering service {ServiceName} with ID {ServiceId}.", _serviceName, _serviceId);
        }
    }

    public override async Task StopAsync(CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("Deregistering service {ServiceName} with ID {ServiceId}", _serviceName, _serviceId);
            await _consulClient.Agent.ServiceDeregister(_serviceId);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while deregistering service {ServiceName} with ID {ServiceId}.", _serviceName, _serviceId);
        }
    }
}