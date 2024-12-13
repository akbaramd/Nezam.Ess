using Nezam.Modular.ESS.Units.Domain.Units;
using Payeh.SharedKernel.EntityFrameworkCore.Domain;
using Payeh.SharedKernel.EntityFrameworkCore.UnitOfWork;

namespace Nezam.Modular.ESS.Infrastructure.Data.Repository;

public class UnitRepository : EntityFrameworkRepository<UnitEntity,AppDbContext> ,IUnitRepository
{
    public UnitRepository(IUnitOfWorkManager work) : base(work)
    {
    }
}