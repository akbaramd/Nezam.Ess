using Bonyan.EntityFrameworkCore;
using Bonyan.Job.Hangfire;
using Bonyan.Modularity;
using FastEndpoints;
using FastEndpoints.Swagger;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Nezam.Modular.ESS.Infrastructure;

namespace Nezam.Modular.ESS.WebApi;


public class NezamEssModule : BonWebModule
{
    public NezamEssModule()
    {
        DependOn<BonAspNetCoreWorkersHangfireModule>();
        DependOn<NezamEssIdEntityInfrastructureModule>();
    }

    public override Task OnConfigureAsync(BonConfigurationContext context)
    {
        context.Services.Configure<BonEntityFrameworkDbContextOptions>(c =>
        {
            c.UseSqlite("Data Source=./NezamEes.db");
        });
        
        return base.OnConfigureAsync(context);
    }

    
    
    public override Task OnApplicationAsync(BonContext context)
    {
        context.Application.UseBonyanExceptionHandling();
        return base.OnApplicationAsync(context);
    }

    public override Task OnPostApplicationAsync(BonContext context)
    {
        context.Application.UseHttpsRedirection();
        context.Application.UseAuthentication();
        context.Application.UseAuthorization();

// Use the localization settings configured
        var locOptions = context.Application.Services.GetRequiredService<IOptions<RequestLocalizationOptions>>();
        context.Application.UseRequestLocalization(locOptions.Value);

        context.Application
            .UseFastEndpoints()
            .UseSwaggerGen();

        return base.OnPostApplicationAsync(context);
    }
}