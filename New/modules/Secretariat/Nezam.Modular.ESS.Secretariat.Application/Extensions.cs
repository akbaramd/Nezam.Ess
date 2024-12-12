using Microsoft.Extensions.DependencyInjection;
using Nezam.Modular.ESS.Secretariat.Domain;

namespace Nezam.Modular.ESS.Secretariat.Application;

public static class Extensions  
{
   public static  IServiceCollection AddSecretariatsApplication(this IServiceCollection services)
   {
      services.AddSecretariatsDomain();
      return services;
   }
   
}