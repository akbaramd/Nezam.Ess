using Nezam.Modular.ESS.Units.Domain.Units;
using Payeh.SharedKernel.EntityFrameworkCore.Domain;

namespace Nezam.Modular.ESS.Infrastructure.Data.Repository;

public class UnitRepository : EntityFrameworkRepository<UnitEntity,AppDbContext> ,IUnitRepository
{
    public UnitRepository(AppDbContext context) : base(context)
    {
    }
}