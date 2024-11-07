using Bonyan.EntityFrameworkCore;
using Bonyan.Job.Hangfire;
using Bonyan.Modularity;
using Microsoft.EntityFrameworkCore;
using Nezam.Modular.ESS.Infrastructure;

namespace Nezam.Modular.ESS.WebApi;


public class NezamEssModule : WebModule
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
}