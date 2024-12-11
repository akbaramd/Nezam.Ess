using Bonyan.Layer.Domain;
using Nezam.Modular.ESS.Identity.Domain.Roles;
using Nezam.Modular.ESS.Identity.Domain.Shared.Roles;

namespace Nezam.Modular.ESS.Infrastructure.Data.Repository;

public class RoleRepository : EfCoreBonRepository<RoleEntity,IdentityDbContext>, IRoleRepository
{




    public Task<RoleEntity> GetRoleByIdAsync(RoleId roleId)
    {
        return GetOneAsync(x=>x.RoleId == roleId);
    }
}