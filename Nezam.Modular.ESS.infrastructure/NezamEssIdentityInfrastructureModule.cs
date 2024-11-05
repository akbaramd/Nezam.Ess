using Bonyan.EntityFrameworkCore;
using Bonyan.Modularity;
using Bonyan.UserManagement.Domain.Enumerations;
using Bonyan.UserManagement.Domain.ValueObjects;
using Bonyan.UserManagement.EntityFrameworkCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nezam.Modular.ESS.Identity.Application;
using Nezam.Modular.ESS.Identity.Domain.Employer;
using Nezam.Modular.ESS.Identity.Domain.Engineer;
using Nezam.Modular.ESS.Identity.Domain.Roles;
using Nezam.Modular.ESS.Identity.Domain.User;
using Nezam.Modular.ESS.infrastructure.Data;
using Nezam.Modular.ESS.infrastructure.Data.Repository;

namespace Nezam.Modular.ESS.infrastructure;

public class NezamEssIdentityInfrastructureModule : WebModule
{
    public NezamEssIdentityInfrastructureModule()
    {
        DependOn<BonyanUserManagementEntityFrameworkModule<UserEntity>>();
        DependOn<NezamEssIdentityApplicationModule>();
    }

    public override Task OnConfigureAsync(ServiceConfigurationContext context)
    {
        context.AddBonyanDbContext<IdentityDbContext>(c => { c.AddDefaultRepositories(true); });
        context.Services.AddTransient<IRoleRepository, RoleRepository>();
        context.Services.AddTransient<IUserRepository, UserRepository>();
        context.Services.AddTransient<IUserVerificationTokenRepository, UserVerificationTokenRepository>();
        context.Services.AddTransient<IEmployerRepository, EmployerRepository>();
        context.Services.AddTransient<IEngineerRepository, EngineerRepository>();

        var configuration = context.Services.BuildServiceProvider().GetRequiredService<IConfiguration>() ??
                            throw new ArgumentNullException("context.Services.GetRequiredService<IConfiguration>()");

       

        return base.OnConfigureAsync(context);
    }

    public override Task OnPreApplicationAsync(ApplicationContext context)
    {
        context.Application.UseAuthentication();
        return base.OnPreApplicationAsync(context);
    }

    public override async Task OnPostApplicationAsync(ApplicationContext context)
    {
        await SeedRoles(context.RequireService<IRoleRepository>());
        await SeedUser(context.RequireService<IUserRepository>());

        await base.OnPostApplicationAsync(context);
    }

    private async Task SeedUser(IUserRepository requireService)
    {
        if (!await requireService.ExistsAsync(x => x.UserName.Equals("akbarsafari00")))
        {
            var user = new UserEntity(UserId.CreateNew(), "akbarsafari00");
            user.SetEmail(new Email("akbarsafari00@gmail.com"));
            user.VerifyEmail();
            user.SetPhoneNumber(new PhoneNumber("9371770774"));
            user.VerifyPhoneNumber();
            user.ChangeStatus(UserStatus.Active);
            user.SetPassword("Aa@123456");
            await requireService.AddAsync(user);
        }
    }

    private async Task SeedRoles(IRoleRepository requireService)
    {
        if (!await requireService.ExistsAsync(x => x.Name.Equals("admin")))
        {
            await requireService.AddAsync(new RoleEntity(RoleId.CreateNew(), "admin", "میدریت"));
        }
    }
}