﻿using Microsoft.Extensions.DependencyInjection;
using Nezam.Modular.ESS.Identity.Domain.Roles;
using Nezam.Modular.ESS.Identity.Domain.User;

namespace Nezam.Modular.ESS.Identity.Domain;

public static class Extensions  
{
   public static  IServiceCollection AddIdentityDomain(this IServiceCollection services)
   {
      services.AddTransient<IUserDomainService, UserDomainService>();
      services.AddTransient<IRoleDomainService,RoleDomainService>();
      return services;
   }
   
}