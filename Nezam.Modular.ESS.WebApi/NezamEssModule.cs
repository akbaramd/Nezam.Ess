using Bonyan.Modularity;
using Bonyan.MultiTenant;
using Microsoft.Extensions.Options;
using Nezam.Modular.ESS.Infrastructure;

namespace Nezam.Modular.ESS.WebApi;


public class NezamEssModule : BonWebModule
{
    public NezamEssModule()
    {
        DependOn<NezamEssIdentityInfrastructureModule>();
        DependOn<BonMultiTenantModule>();
    }

    public override Task OnConfigureAsync(BonConfigurationContext context)
    {
        return base.OnConfigureAsync(context);
    }

    public override Task OnApplicationAsync(BonWebApplicationContext context)
    {
        context.Application.UseBonyanExceptionHandling();
        return base.OnApplicationAsync(context);
    }

    public override Task OnPostApplicationAsync(BonWebApplicationContext context)
    {
        context.Application.UseHttpsRedirection();
     

// Use the localization settings configured
        var locOptions = context.Application.Services.GetRequiredService<IOptions<RequestLocalizationOptions>>();
        context.Application.UseRequestLocalization(locOptions.Value);

     

        return base.OnPostApplicationAsync(context);
    }
}