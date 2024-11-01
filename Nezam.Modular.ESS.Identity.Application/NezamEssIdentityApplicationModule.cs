using Bonyan.Modularity;
using Bonyan.Modularity.Abstractions;
using Nezam.Modular.ESS.Identity.Domain;

namespace Nezam.Modular.ESS.Identity.Application;

public class NezamEssIdentityApplicationModule : Module
{
    public NezamEssIdentityApplicationModule()
    {
        DependOn<NezamEssIdentityDomainModule>();
    }
    
    public override Task OnConfigureAsync(ServiceConfigurationContext context)
    {
        return base.OnConfigureAsync(context);
    }
}