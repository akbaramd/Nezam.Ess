using Microsoft.Extensions.DependencyInjection;
using Nezam.Modular.ESS.Units.Domain;

namespace Nezam.Modular.ESS.Units.Application;

public static class Extensions  
{
   public static  IServiceCollection AddUnitsApplication(this IServiceCollection services)
   {
      services.AddUnitsDomain();
      return services;
   }
   
}