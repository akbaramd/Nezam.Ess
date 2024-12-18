﻿using Microsoft.Extensions.DependencyInjection;
using Nezam.Modular.ESS.Identity.Domain.Employer;
using Nezam.Modular.ESS.Identity.Domain.Engineer;
using Nezam.Modular.ESS.Identity.Domain.Roles;
using Nezam.Modular.ESS.Identity.Domain.User;
using Nezam.Modular.ESS.Infrastructure.Data.Repository;
using Nezam.Modular.ESS.Infrastructure.Data.Seeds;
using Nezam.Modular.ESS.Units.Domain.Member;
using Nezam.Modular.ESS.Units.Domain.Units;

namespace Nezam.Modular.ESS.Infrastructure;

public static class Extensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddHostedService<IdentitySeedService>();
        services.AddMediatR(c => { c.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies()); });

        services.AddTransient<IUserRepository, UserRepository>();
        services.AddTransient<IRoleRepository, RoleRepository>();
        services.AddTransient<IEmployerRepository, EmployerRepository>();
        services.AddTransient<IEngineerRepository, EngineerRepository>();
        // services.AddTransient<IDocumentReadOnlyRepository, DocumentReadOnlyRepository>();
        // services.AddTransient<IDocumentRepository, DocumentRepository>();
        services.AddTransient<IMemberRepository, MemberRepository>();
        services.AddTransient<IUnitRepository, UnitRepository>();
        return services;
    }
}