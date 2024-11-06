using Bonyan.AutoMapper;
using Bonyan.Modularity;
using Bonyan.Modularity.Abstractions;
using Bonyan.UserManagement.Application;
using Microsoft.Extensions.DependencyInjection;
using Nezam.Modular.ESS.Identity.Application.Auth;
using Nezam.Modular.ESS.Identity.Application.Employers;
using Nezam.Modular.ESS.Identity.Application.Employers.Jobs;
using Nezam.Modular.ESS.Identity.Application.Engineers;
using Nezam.Modular.ESS.Identity.Application.Engineers.Jobs;
using Nezam.Modular.ESS.Identity.Application.Roles;
using Nezam.Modular.ESS.Identity.Application.Users;
using Nezam.Modular.ESS.Identity.Domain;
using Nezam.Modular.ESS.Identity.Domain.User;

namespace Nezam.Modular.ESS.Identity.Application;

public class NezamEssIdentityApplicationModule : Module
{
    public NezamEssIdentityApplicationModule()
    {
        DependOn<BonyanUserManagementApplicationModule<UserEntity>>();
        DependOn<NezamEssIdentityDomainModule>();
    }

    public override Task OnConfigureAsync(ServiceConfigurationContext context)
    {
        context.Services.AddTransient<IAuthService, AuthService>();
        context.Services.AddTransient<IRoleService, RoleService>();
        context.Services.AddTransient<IUserService, UserService>();
        context.Services.AddTransient<IEmployerService, EmployerService>();
        context.Services.AddTransient<IEngineerService, EngineerService>();

        context.Services.Configure<BonyanAutoMapperOptions>(c =>
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
        // context.AddJob<EngineerSynchronizerJob>();
        context.AddJob<EmployerSynchronizerJob>();

        return base.OnConfigureAsync(context);
    }
}