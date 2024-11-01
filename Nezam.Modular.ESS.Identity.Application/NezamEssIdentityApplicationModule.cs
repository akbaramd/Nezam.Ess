using Bonyan.Modularity;
using Bonyan.Modularity.Abstractions;
using Bonyan.UserManagement.Application;
using Nezam.Modular.ESS.Identity.Domain;
using Nezam.Modular.ESS.Identity.Domain.User;

namespace Nezam.Modular.ESS.Identity.Application;

public class NezamEssIdentityApplicationModule : Module
{
    public NezamEssIdentityApplicationModule()
    {
        DependOn<BonyanUserManagementApplicationModule<UserEntity>>();
        DependOn<NezamEssIdentityDomainModule>();
    }
    
    public override Task OnConfigureAsync(ServiceConfigurationContext context)
    {
        return base.OnConfigureAsync(context);
    }
}