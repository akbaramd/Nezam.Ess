using Bonyan.DependencyInjection;
using Bonyan.Modularity;
using Bonyan.UserManagement.Domain.Enumerations;
using Bonyan.UserManagement.Domain.ValueObjects;
using Bonyan.UserManagement.EntityFrameworkCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nezam.Modular.ESS.IdEntity.Application;
using Nezam.Modular.ESS.IdEntity.Domain.Employer;
using Nezam.Modular.ESS.IdEntity.Domain.Engineer;
using Nezam.Modular.ESS.IdEntity.Domain.Roles;
using Nezam.Modular.ESS.IdEntity.Domain.User;
using Nezam.Modular.ESS.Infrastructure.Data;
using Nezam.Modular.ESS.Infrastructure.Data.Repository;
using Nezam.Modular.ESS.Secretariat.Application;
using Nezam.Modular.ESS.Secretariat.Domain.Documents.Repositories;

namespace Nezam.Modular.ESS.Infrastructure;

public class NezamEssIdEntityInfrastructureModule : WebModule
{
    public NezamEssIdEntityInfrastructureModule()
    {
        DependOn<BonUserManagementEntityFrameworkModule<UserEntity>>();
        DependOn<NezamEssIdEntityApplicationModule>();
        DependOn<NezamEssSecretariatApplicationModule>();
    }

    public override Task OnConfigureAsync(BonConfigurationContext context)
    {
        context.AddBonDbContext<IdEntityDbContext>(c => { c.AddDefaultRepositories(true); });
        context.Services.AddTransient<IRoleRepository, RoleRepository>();
        context.Services.AddTransient<IUserRepository, UserRepository>();
        context.Services.AddTransient<IUserVerificationTokenRepository, UserVerificationTokenRepository>();
        context.Services.AddTransient<IEmployerRepository, EmployerRepository>();
        context.Services.AddTransient<IEngineerRepository, EngineerRepository>();
        context.Services.AddTransient<IDocumentReadOnlyRepository, DocumentReadOnlyRepository>();
        context.Services.AddTransient<IDocumentRepository, DocumentRepository>();

        var configuration = context.Services.BuildServiceProvider().GetRequiredService<IConfiguration>() ??
                            throw new ArgumentNullException("context.Services.GetRequiredService<IConfiguration>()");

       

        return base.OnConfigureAsync(context);
    }

    public override Task OnPreApplicationAsync(BonContext context)
    {
        context.Application.UseAuthentication();
        return base.OnPreApplicationAsync(context);
    }

    public override async Task OnPostApplicationAsync(BonContext context)
    {
        await SeedRoles(context.RequireService<IRoleRepository>());
        await SeedUser(context.RequireService<IUserRepository>());

        await base.OnPostApplicationAsync(context);
    }

    private async Task SeedUser(IUserRepository requireService)
    {
        if (!await requireService.ExistsAsync(x => x.UserName.Equals("akbarsafari00")))
        {
            var user = UserEntity.Create(BonUserId.CreateNew(), "akbarsafari00");
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