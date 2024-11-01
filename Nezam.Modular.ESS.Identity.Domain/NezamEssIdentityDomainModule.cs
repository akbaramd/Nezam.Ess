using Bonyan.Modularity;
using Bonyan.Modularity.Abstractions;
using Bonyan.UserManagement.Domain;
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
        return base.OnConfigureAsync(context);
    }
}