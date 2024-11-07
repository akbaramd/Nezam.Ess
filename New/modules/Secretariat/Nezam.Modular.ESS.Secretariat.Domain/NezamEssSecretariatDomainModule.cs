using Bonyan.Modularity;
using Bonyan.Modularity.Abstractions;

namespace Nezam.Modular.ESS.Secretariat.Domain;

public class NezamEssSecretariatDomainModule : BonModule
{
    public NezamEssSecretariatDomainModule()
    {
    }
    public override Task OnConfigureAsync(BonConfigurationContext context)
    {
        return base.OnConfigureAsync(context);
    }
}