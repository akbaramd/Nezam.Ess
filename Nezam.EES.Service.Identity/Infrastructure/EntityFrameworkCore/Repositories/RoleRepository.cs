using Nezam.EES.Service.Identity.Domains.Roles;
using Nezam.EES.Service.Identity.Domains.Roles.Repositories;
using Nezam.EEs.Shared.Domain.Identity.Roles;
using Payeh.SharedKernel.EntityFrameworkCore.Domain;
using Payeh.SharedKernel.EntityFrameworkCore.UnitofWork;

namespace Nezam.EES.Service.Identity.Infrastructure.EntityFrameworkCore.Repositories;

public class RoleRepository : EntityFrameworkRepository<RoleEntity,IIdentityDbContext>, IRoleRepository
{

    public Task<RoleEntity?> FindRoleByIdAsync(RoleId roleId)
    {
        return FindOneAsync(x=>x.RoleId == roleId);
    }

    public RoleRepository(IEfCoreDbContextProvider<IIdentityDbContext> contextProvider) : base(contextProvider)
    {
    }
}