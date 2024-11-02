using Bonyan.AspNetCore.Job;
using Bonyan.EntityFrameworkCore;
using Bonyan.Job.Hangfire;
using Bonyan.Modularity;
using Microsoft.EntityFrameworkCore;
using Nezam.Modular.ESS.Identity.infrastructure;

namespace Nezam.Modular.ESS.WebApi;


public class NezamEssModule : WebModule
{
    public NezamEssModule()
    {
        DependOn<BonyanJobModule>();
        DependOn<NezamEssIdentityInfrastructureModule>();
    }

    public override Task OnConfigureAsync(ServiceConfigurationContext context)
    {
        context.Services.Configure<EntityFrameworkDbContextOptions>(c =>
        {
            c.UseSqlite("Data Source=./NezamEes.db");
        });
        
        return base.OnConfigureAsync(context);
    }


    public override Task OnApplicationAsync(ApplicationContext context)
    {
        context.Application.UseBonyanExceptionHandling();
        return base.OnApplicationAsync(context);
    }
}