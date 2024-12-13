using Nezam.Modular.ESS.Identity.Domain.Employer;
using Payeh.SharedKernel.EntityFrameworkCore.Domain;
using Payeh.SharedKernel.EntityFrameworkCore.UnitOfWork;

namespace Nezam.Modular.ESS.Infrastructure.Data.Repository;

public class EmployerRepository : EntityFrameworkRepository<EmployerEntity, AppDbContext>, IEmployerRepository
{
    public EmployerRepository(IUnitOfWorkManager work) : base(work)
    {
    }
}