using Bonyan.Layer.Domain;
using Nezam.Modular.ESS.Units.Domain.Units;

namespace Nezam.Modular.ESS.Infrastructure.Data.Repository;

public class UnitRepository : EfCoreBonRepository<UnitEntity,IdentityDbContext> ,IUnitRepository
{
    
}