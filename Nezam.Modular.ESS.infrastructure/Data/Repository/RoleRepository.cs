using Nezam.Modular.ESS.Identity.Domain.Roles;
using Nezam.Modular.ESS.Identity.Domain.Shared.Roles;
using Payeh.SharedKernel.EntityFrameworkCore.Domain;

namespace Nezam.Modular.ESS.Infrastructure.Data.Repository;

public class RoleRepository : EntityFrameworkRepository<RoleEntity,AppDbContext>, IRoleRepository
{
    public RoleRepository(AppDbContext context) : base(context)
    {
    }

    public Task<RoleEntity> GetRoleByIdAsync(RoleId roleId)
    {
        return GetOneAsync(x=>x.RoleId == roleId);
    }
}