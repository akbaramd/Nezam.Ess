using Bonyan.Modularity;
using Bonyan.Modularity.Abstractions;
using Bonyan.UserManagement.Domain;
using Microsoft.Extensions.DependencyInjection;
using Nezam.Modular.ESS.Identity.Domain.Roles;
using Nezam.Modular.ESS.Identity.Domain.User;

namespace Nezam.Modular.ESS.Identity.Domain;

public class NezamEssIdEntityDomainModule : BonModule
{
    public NezamEssIdEntityDomainModule()
    {
        DependOn<BonUserManagementDomainModule<UserEntity>>();
    }
    public override Task OnConfigureAsync(BonConfigurationContext context)
    {
        context.Services.AddTransient<UserDomainService>();
        context.Services.AddTransient<RoleManager>();
        return base.OnConfigureAsync(context);
    }
}