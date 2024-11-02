using Bonyan.Layer.Domain;
using Nezam.Modular.ESS.Identity.Domain.Employer;
using Nezam.Modular.ESS.Identity.Domain.Engineer;
using Nezam.Modular.ESS.Identity.Domain.Roles;

namespace Nezam.Modular.ESS.Identity.infrastructure.Data.Repository;

public class EngineerRepository : EfCoreRepository<EngineerEntity,EngineerId, IdentityDbContext>, IEngineerRepository
{
    public EngineerRepository(IdentityDbContext userManagementDbContext) : base(userManagementDbContext)
    {
    }
}