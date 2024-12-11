using Bonyan.Modularity;
using Bonyan.Modularity.Abstractions;
// using Nezam.Modular.ESS.Identity.Application.Employers;
// using Nezam.Modular.ESS.Identity.Application.Employers.Jobs;
// using Nezam.Modular.ESS.Identity.Application.Engineers;
// using Nezam.Modular.ESS.Identity.Application.Engineers.Jobs;
// using Nezam.Modular.ESS.Identity.Application.Users;
using Nezam.Modular.ESS.Identity.Domain;

namespace Nezam.Modular.ESS.Identity.Application;

public class NezamEssIdentityApplicationModule : BonModule
{
    public NezamEssIdentityApplicationModule()
    {
        DependOn<NezamEssIdEntityDomainModule>();
    }

    public override Task OnConfigureAsync(BonConfigurationContext context)
    {
        // context.Services.AddHostedService<EmployerSynchronizerJob>();
        
        // context.Services.AddTransient<IUserService, UserService>();
        // context.Services.AddTransient<IEmployerService, EmployerService>();
        // context.Services.AddTransient<IEngineerService, EngineerService>();

        // context.Services.Configure<BonAutoMapperOptions>(c =>
        // {
            // c.AddProfile<UserProfile>();
            // c.AddProfile<EngineerProfile>();
            // c.AddProfile<EmployerProfile>();
        // });
        // context.Services.AddTransient<UserProfile>();
        // context.Services.AddTransient<EngineerProfile>();
        // context.Services.AddTransient<EmployerProfile>();

        // context.Services.AddSingleton<EmployerSynchronizerJob>();
        // context.Services.AddSingleton<EngineerSynchronizerWorker>();
        return base.OnConfigureAsync(context);
    }

    public override Task OnInitializeAsync(BonInitializedContext context)
    {
        return base.OnInitializeAsync(context);
    }
}