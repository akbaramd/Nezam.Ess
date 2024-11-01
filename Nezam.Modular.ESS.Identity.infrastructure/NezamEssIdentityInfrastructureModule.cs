using Bonyan.EntityFrameworkCore;
using Bonyan.Modularity;
using Bonyan.UserManagement.EntityFrameworkCore;
using Nezam.Modular.ESS.Identity.Application;
using Nezam.Modular.ESS.Identity.Domain.User;
using Nezam.Modular.ESS.Identity.infrastructure.Data;

namespace Nezam.Modular.ESS.Identity.infrastructure;

public class NezamEssIdentityInfrastructureModule : WebModule
{
    public NezamEssIdentityInfrastructureModule()
    {
        DependOn<BonyanUserManagementEntityFrameworkModule<UserEntity>>();
        DependOn<NezamEssIdentityApplicationModule>();
    }
    
    public override Task OnConfigureAsync(ServiceConfigurationContext context)
    {
        context.AddBonyanDbContext<IdentityDbContext>();
        
        return base.OnConfigureAsync(context);
    }
}