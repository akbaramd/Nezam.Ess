using Bonyan.Modularity;
using Bonyan.Modularity.Abstractions;
using Bonyan.UserManagement.Domain;
using Microsoft;
using Microsoft.Extensions.DependencyInjection;
using Nezam.Modular.ESS.Identity.Domain.DomainServices;
using Nezam.Modular.ESS.Identity.Domain.User;

namespace Nezam.Modular.ESS.Identity.Domain;

public class NezamEssIdentityDomainModule : Module
{
    public NezamEssIdentityDomainModule()
    {
        DependOn<BonyanUserManagementDomainModule<UserEntity>>();
    }
    public override Task OnConfigureAsync(ServiceConfigurationContext context)
    {
        context.Services.AddTransient<UserManager>();
        context.Services.AddTransient<RoleManager>();
        return base.OnConfigureAsync(context);
    }
}