using Bonyan.Layer.Domain;
using Nezam.Modular.ESS.Identity.Domain.Engineer;
using Nezam.Modular.ESS.Identity.Domain.Shared.Engineer;

namespace Nezam.Modular.ESS.Infrastructure.Data.Repository;

public class EngineerRepository : EfCoreBonRepository<EngineerEntity,EngineerId, IdentityDbContext>, IEngineerRepository
{
    public EngineerRepository(IdentityDbContext userManagementDbContext) : base(userManagementDbContext)
    {
    }
}