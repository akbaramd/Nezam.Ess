using Bonyan.IdentityManagement.Options;
using Bonyan.IdentityManagement.WebApi;
using Bonyan.Modularity;
using Bonyan.MultiTenant;
using FastEndpoints;
using FastEndpoints.Swagger;
using Microsoft.Extensions.Options;
using Nezam.Modular.ESS.Identity.Domain.User;
using Nezam.Modular.ESS.Infrastructure;

namespace Nezam.Modular.ESS.WebApi;


public class NezamEssModule : BonWebModule
{
    public NezamEssModule()
    {
        DependOn<NezamEssIdEntityInfrastructureModule>();
        DependOn<BonMultiTenantModule>();
    }

    public override Task OnConfigureAsync(BonConfigurationContext context)
    {
        PreConfigure<BonAuthenticationJwtOptions>(c =>
        {
            c.SecretKey = "sddasdq3eqwdadawedwad09w7243y42h492u3g59u2395uh29u3h5u235";
            c.Audience = "testestset";
            c.Issuer = "testestset";
            c.ExpirationInMinutes = 5;
            c.Enabled = true;
        });
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

        context.Application
            .UseFastEndpoints()
            .UseSwaggerGen();

        return base.OnPostApplicationAsync(context);
    }
}