using Bonyan.Modularity;
using Bonyan.Modularity.Abstractions;

namespace Nezam.Modular.ESS.Units.Domain;

public class NezamEssUnitsDomainModule : BonModule
{
    public NezamEssUnitsDomainModule()
    {
    }
    public override Task OnConfigureAsync(BonConfigurationContext context)
    {
        return base.OnConfigureAsync(context);
    }
}