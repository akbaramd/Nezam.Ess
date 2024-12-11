using Bonyan.DependencyInjection;
using Bonyan.IdentityManagement.EntityFrameworkCore;
using Bonyan.Modularity;
using Bonyan.UserManagement.Domain.Users.Enumerations;
using Bonyan.UserManagement.Domain.Users.ValueObjects;
using Bonyan.UserManagement.EntityFrameworkCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nezam.Modular.ESS.Identity.Application;
using Nezam.Modular.ESS.Identity.Domain.Employer;
using Nezam.Modular.ESS.Identity.Domain.Engineer;
using Nezam.Modular.ESS.Identity.Domain.Roles;
using Nezam.Modular.ESS.Identity.Domain.Shared.Roles;
using Nezam.Modular.ESS.Identity.Domain.User;
using Nezam.Modular.ESS.Infrastructure.Data;
using Nezam.Modular.ESS.Infrastructure.Data.Repository;
using Nezam.Modular.ESS.Secretariat.Application;
using Nezam.Modular.ESS.Secretariat.Domain.Documents.Repositories;

namespace Nezam.Modular.ESS.Infrastructure;

public class NezamEssIdEntityInfrastructureModule : BonWebModule
{
    public NezamEssIdEntityInfrastructureModule()
    {
 
        DependOn<NezamEssIdentityApplicationModule>();
        DependOn<NezamEssSecretariatApplicationModule>();
    }

    public override Task OnConfigureAsync(BonConfigurationContext context)
    {
        context.AddDbContext<IdentityDbContext>(c =>
        {
            c.UseSqlite("Data Source=NezamEes.db;Mode=ReadWrite;");
            c.AddDefaultRepositories(true);
        });
        context.Services.AddTransient<IUserRepository, UserRepository>();
        context.Services.AddTransient<IRoleRepository, RoleRepository>();
        context.Services.AddTransient<IEmployerRepository, EmployerRepository>();
        context.Services.AddTransient<IEngineerRepository, EngineerRepository>();
        context.Services.AddTransient<IDocumentReadOnlyRepository, DocumentReadOnlyRepository>();
        context.Services.AddTransient<IDocumentRepository, DocumentRepository>();

        var configuration = context.Services.BuildServiceProvider().GetRequiredService<IConfiguration>() ??
                            throw new ArgumentNullException("context.Services.GetRequiredService<IConfiguration>()");

       

        return base.OnConfigureAsync(context);
    }

    public override Task OnPreApplicationAsync(BonWebApplicationContext context)
    {
        context.Application.UseAuthentication();
        return base.OnPreApplicationAsync(context);
    }

    public override async Task OnPostApplicationAsync(BonWebApplicationContext context)
    {
        //await SeedRoles(context.RequireService<IRoleRepository>());
        //await SeedUser(context.RequireService<IRoleRepository>());

        await base.OnPostApplicationAsync(context);
    }

    // private async Task SeedUser(IRoleRepository requireService)
    // {
    //     if (!await requireService.ExistsAsync(x => x.UserName.Equals("akbarsafari00")))
    //     {
    //         var user = UserEntity.Create(UserId.NewId(), "akbarsafari00");
    //         user.SetEmail(new BonUserEmail("akbarsafari00@gmail.com"));
    //         user.VerifyEmail();
    //         user.SetPhoneNumber(new BonUserPhoneNumber("9371770774"));
    //         user.VerifyPhoneNumber();
    //         user.ChangeStatus(UserStatus.Active);
    //         user.SetPassword("Aa@123456");
    //         await requireService.AddAsync(user);
    //     }
    // }

   
}