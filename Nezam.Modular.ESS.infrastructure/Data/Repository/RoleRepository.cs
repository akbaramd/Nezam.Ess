using Bonyan.Layer.Domain;
using Nezam.Modular.ESS.Identity.Domain.Roles;

namespace Nezam.Modular.ESS.Infrastructure.Data.Repository;

public class RoleRepository : EfCoreRepository<RoleEntity, RoleId, IdentityDbContext>, IRoleRepository
{
    public RoleRepository(IdentityDbContext userManagementDbContext) : base(userManagementDbContext)
    {
    }
}