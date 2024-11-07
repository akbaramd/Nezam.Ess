using Bonyan.AutoMapper;
using Bonyan.DependencyInjection;
using Bonyan.Modularity;
using Bonyan.Modularity.Abstractions;
using Bonyan.UserManagement.Application;
using Microsoft.Extensions.DependencyInjection;
using Nezam.Modular.ESS.IdEntity.Application.Auth;
using Nezam.Modular.ESS.IdEntity.Application.Employers;
using Nezam.Modular.ESS.IdEntity.Application.Employers.Jobs;
using Nezam.Modular.ESS.IdEntity.Application.Engineers;
using Nezam.Modular.ESS.Identity.Application.Engineers.Jobs;
using Nezam.Modular.ESS.IdEntity.Application.Roles;
using Nezam.Modular.ESS.IdEntity.Application.Users;
using Nezam.Modular.ESS.IdEntity.Domain;
using Nezam.Modular.ESS.IdEntity.Domain.User;

namespace Nezam.Modular.ESS.IdEntity.Application;

public class NezamEssIdEntityApplicationModule : BonModule
{
    public NezamEssIdEntityApplicationModule()
    {
        DependOn<BonUserManagementApplicationModule<UserEntity>>();
        DependOn<NezamEssIdEntityDomainModule>();
    }

    public override Task OnConfigureAsync(BonConfigurationContext context)
    {
        context.Services.AddTransient<IAuthService, AuthService>();
        context.Services.AddTransient<IRoleService, RoleService>();
        context.Services.AddTransient<IUserService, UserService>();
        context.Services.AddTransient<IEmployerService, EmployerService>();
        context.Services.AddTransient<IEngineerService, EngineerService>();

        context.Services.Configure<BonAutoMapperOptions>(c =>
        {
            c.AddProfile<AuthProfile>();
            c.AddProfile<RoleProfile>();
            c.AddProfile<UserProfile>();
            c.AddProfile<EngineerProfile>();
            c.AddProfile<EmployerProfile>();
        });
        context.Services.AddTransient<AuthProfile>();
        context.Services.AddTransient<RoleProfile>();
        context.Services.AddTransient<UserProfile>();
        context.Services.AddTransient<EngineerProfile>();
        context.Services.AddTransient<EmployerProfile>();

        context.Services.AddSingleton<EmployerSynchronizerJob>();
        context.Services.AddSingleton<EngineerSynchronizerWorker>();
        return base.OnConfigureAsync(context);
    }

    public override Task OnInitializeAsync(ServiceInitializationContext context)
    {
        context.AddCronWorkerAsync<EmployerSynchronizerJob>("*/1 * * * *");
        context.AddCronWorkerAsync<EngineerSynchronizerWorker>("*/1 * * * *");
        return base.OnInitializeAsync(context);
    }
}