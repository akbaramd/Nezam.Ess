using Bonyan.AutoMapper;
using Bonyan.Modularity;
using Bonyan.Modularity.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using Nezam.Modular.ESS.Secretariat.Application.Documents;
using Nezam.Modular.ESS.Secretariat.Domain;

namespace Nezam.Modular.ESS.Secretariat.Application;

public class NezamEssSecretariatApplicationModule : BonModule
{
    public NezamEssSecretariatApplicationModule()
    {
        DependOn<NezamEssSecretariatDomainModule>();
    }
    public override Task OnConfigureAsync(BonConfigurationContext context)
    {
        context.Services.AddTransient<IDocumentApplicationService, DocumentApplicationService>();
        
        context.Services.Configure<BonAutoMapperOptions>(c =>
        {
            c.AddMaps<NezamEssSecretariatApplicationModule>();
        });
        
        context.Services.AddLocalization();
        return base.OnConfigureAsync(context);
    }
}