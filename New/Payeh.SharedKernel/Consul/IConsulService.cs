using Consul;

namespace Payeh.SharedKernel.Consul;

public interface IConsulService
{
    Task<IEnumerable<AgentService>> GetAllServicesAsync();  // برای دریافت تمام سرویس‌ها
    Task<AgentService> GetServiceDetailsAsync(string serviceName);  // برای دریافت جزئیات یک سرویس خاص

    Task<ServiceEntry[]> GetServiceHealthAsync(string serviceName);
    // دریافت وضعیت سلامت تمام سرویس‌ها
    Task<List<ServiceEntry>> GetCurrentNodeHealthAsync();
}