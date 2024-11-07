using Bonyan.Layer.Domain;
using Nezam.Modular.ESS.IdEntity.Domain.Engineer;

namespace Nezam.Modular.ESS.Infrastructure.Data.Repository;

public class EngineerRepository : EfCoreBonRepository<EngineerEntity,EngineerId, IdEntityDbContext>, IEngineerRepository
{
    public EngineerRepository(IdEntityDbContext userManagementDbContext) : base(userManagementDbContext)
    {
    }
}