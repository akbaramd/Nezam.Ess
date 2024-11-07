using Bonyan.Modularity;
using Bonyan.Modularity.Abstractions;
using Bonyan.UserManagement.Domain;
using Microsoft.Extensions.DependencyInjection;
using Nezam.Modular.ESS.IdEntity.Domain.DomainServices;
using Nezam.Modular.ESS.IdEntity.Domain.User;

namespace Nezam.Modular.ESS.IdEntity.Domain;

public class NezamEssIdEntityDomainModule : BonModule
{
    public NezamEssIdEntityDomainModule()
    {
        DependOn<BonUserManagementDomainModule<UserEntity>>();
    }
    public override Task OnConfigureAsync(BonConfigurationContext context)
    {
        context.Services.AddTransient<UserManager>();
        context.Services.AddTransient<RoleManager>();
        return base.OnConfigureAsync(context);
    }
}