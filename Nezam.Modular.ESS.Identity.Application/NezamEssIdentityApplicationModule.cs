using Bonyan.Modularity;
using Bonyan.Modularity.Abstractions;
using Bonyan.UserManagement.Application;
using Microsoft.Extensions.DependencyInjection;
using Nezam.Modular.ESS.Identity.Application.Auth;
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
        return base.OnConfigureAsync(context);
    }
}