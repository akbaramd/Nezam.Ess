using Bonyan.Layer.Domain;
using Nezam.Modular.ESS.Identity.Domain.Engineer;

namespace Nezam.Modular.ESS.infrastructure.Data.Repository;

public class EngineerRepository : EfCoreRepository<EngineerEntity,EngineerId, IdentityDbContext>, IEngineerRepository
{
    public EngineerRepository(IdentityDbContext userManagementDbContext) : base(userManagementDbContext)
    {
    }
}