using Bonyan.Layer.Domain;
using Nezam.Modular.ESS.Identity.Domain.Roles;
using Nezam.Modular.ESS.Identity.Domain.Shared.Roles;

namespace Nezam.Modular.ESS.Infrastructure.Data.Repository;

public class RoleRepository : EfCoreBonRepository<RoleEntity, RoleId, IdentityDbContext>, IRoleRepository
{
    public RoleRepository(IdentityDbContext userManagementDbContext) : base(userManagementDbContext)
    {
    }
}