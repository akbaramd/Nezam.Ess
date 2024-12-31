using Nezam.EES.Service.Identity.Domains.Roles.DomainServices;
using Nezam.EES.Service.Identity.Domains.Roles.Repositories;
using Nezam.EES.Service.Identity.Domains.Users.DomainServices;
using Nezam.EES.Service.Identity.Domains.Users.Repositories;
using Nezam.EES.Service.Identity.Infrastructure.EntityFrameworkCore.Repositories;
using Nezam.EES.Service.Identity.Infrastructure.EntityFrameworkCore.Seeds;

namespace Nezam.EES.Service.Identity;

public static class IdentitySliceExtensions
{
    public static IServiceCollection AddIdentitySlice(this IServiceCollection services,IConfiguration configuration)
    {
        // Domains
        services.AddTransient<IUserDomainService, UserDomainService>();
        services.AddTransient<IRoleDomainService, RoleDomainService>();
        
        services.AddHostedService<IdentitySeedService>();
        services.AddTransient<IUserRepository, UserRepository>();
        services.AddTransient<IRoleRepository, RoleRepository>();
        return services;
    }
}