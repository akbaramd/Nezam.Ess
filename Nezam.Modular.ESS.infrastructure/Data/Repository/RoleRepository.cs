using Bonyan.Layer.Domain;
using Nezam.Modular.ESS.IdEntity.Domain.Roles;

namespace Nezam.Modular.ESS.Infrastructure.Data.Repository;

public class RoleRepository : EfCoreBonRepository<RoleEntity, RoleId, IdEntityDbContext>, IRoleRepository
{
    public RoleRepository(IdEntityDbContext userManagementDbContext) : base(userManagementDbContext)
    {
    }
}