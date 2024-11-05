using Bonyan.Modularity;
using Bonyan.Modularity.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace Nezam.Modular.ESS.Secretariat.Domain;

public class NezamEssSecretariatDomainModule : Module
{
    public NezamEssSecretariatDomainModule()
    {
    }
    public override Task OnConfigureAsync(ServiceConfigurationContext context)
    {
        return base.OnConfigureAsync(context);
    }
}