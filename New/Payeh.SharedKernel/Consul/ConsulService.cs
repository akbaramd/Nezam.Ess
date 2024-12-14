using Consul;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Payeh.SharedKernel.Consul
{
    public class ConsulService : IConsulService
    {
        private readonly IConsulClient _consulClient;

        public ConsulService(IConsulClient consulClient)
        {
            _consulClient = consulClient;
        }

        // دریافت تمام سرویس‌های ثبت‌شده در Consul
        public async Task<IEnumerable<AgentService>> GetAllServicesAsync()
        {
            var services = await _consulClient.Agent.Services();
            return services.Response.Values; // تمام سرویس‌ها را برمی‌گرداند
        }

        // دریافت جزئیات یک سرویس خاص با نام آن
        public async Task<AgentService> GetServiceDetailsAsync(string serviceName)
        {
            var services = await _consulClient.Agent.Services();
            
            var service = services.Response.Values.FirstOrDefault(s => s.Service.Equals(serviceName, StringComparison.OrdinalIgnoreCase));
            return service; // اگر سرویس پیدا شد، آن را برمی‌گرداند
        }

        // دریافت وضعیت سلامت یک سرویس خاص
        public async Task<ServiceEntry[]> GetServiceHealthAsync(string serviceName)
        {
            // گرفتن وضعیت سلامت سرویس خاص از Consul
            var healthCheck = await _consulClient.Health.Service(serviceName);

            // HealthServiceResponse شامل اطلاعات وضعیت سلامت می‌باشد
            return healthCheck.Response; // اگر وضعیت سلامت پیدا شد، آن را برمی‌گرداند
        }

        // دریافت وضعیت سلامت تمام سرویس‌ها


        // دریافت وضعیت سلامت نود کنونی
        public async Task<List<ServiceEntry>> GetCurrentNodeHealthAsync()
        {
            // دریافت اطلاعات مربوط به نودها از Consul
            var ser = await _consulClient.Agent.Services();

            List<ServiceEntry> healthChecks = new List<ServiceEntry>();;
            foreach (var value in ser.Response.Values)
            {
                var health = await _consulClient.Health.Service(value.Service); // وضعیت سلامت نود کنونی را می‌گیریم

                healthChecks.AddRange(health.Response);

            }
            
         
            return healthChecks;  // بازگشت وضعیت سلامت سرویس‌ها برای نود
        }
    }
}
