﻿using Bonyan.IdentityManagement.Domain;
using Bonyan.Modularity;
using Bonyan.Modularity.Abstractions;
using Bonyan.UserManagement.Domain;
using Microsoft.Extensions.DependencyInjection;
using Nezam.Modular.ESS.Identity.Domain.User;

namespace Nezam.Modular.ESS.Identity.Domain;

public class NezamEssIdEntityDomainModule : BonModule
{
    public NezamEssIdEntityDomainModule()
    {
   
    }
    public override Task OnConfigureAsync(BonConfigurationContext context)
    {
        context.Services.AddTransient<IUserDomainService, UserDomainService>();
        return base.OnConfigureAsync(context);
    }
}