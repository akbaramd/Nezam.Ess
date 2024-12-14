using Microsoft.Extensions.DependencyInjection;
using Nezam.Modular.ESS.Identity.Domain;

namespace Nezam.Modular.ESS.Identity.Application;

public static class Extensions  
{
   public static  IServiceCollection AddIdentityApplication(this IServiceCollection services)
   {
      // services.AddHostedService<EmployerSynchronizerJob>();
      // services.AddHostedService<EngineerSynchronizerWorker>();
      services.AddIdentityDomain();
      return services;
   }
   
}