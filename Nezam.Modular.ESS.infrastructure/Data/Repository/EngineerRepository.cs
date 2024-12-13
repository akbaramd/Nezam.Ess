using Nezam.Modular.ESS.Identity.Domain.Engineer;
using Payeh.SharedKernel.EntityFrameworkCore.Domain;
using Payeh.SharedKernel.EntityFrameworkCore.UnitOfWork;

namespace Nezam.Modular.ESS.Infrastructure.Data.Repository;

public class EngineerRepository : EntityFrameworkRepository<EngineerEntity, AppDbContext>, IEngineerRepository
{
    public EngineerRepository(IUnitOfWorkManager work) : base(work)
    {
    }
}