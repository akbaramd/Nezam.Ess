using Bonyan.Layer.Domain;
using Bonyan.Modularity;
using Bonyan.Modularity.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using Nezam.Modular.ESS.Identity.Domain.User;

namespace Nezam.Modular.ESS.Identity.Domain;

public class NezamEssIdEntityDomainModule : BonModule
{
    public NezamEssIdEntityDomainModule()
    {
        DependOn<BonLayerDomainModule>();
    }
    public override Task OnConfigureAsync(BonConfigurationContext context)
    {
        context.Services.AddTransient<IUserDomainService, UserDomainService>();
        return base.OnConfigureAsync(context);
    }
}