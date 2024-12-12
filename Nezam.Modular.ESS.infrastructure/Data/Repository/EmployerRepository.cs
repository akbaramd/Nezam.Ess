using Nezam.Modular.ESS.Identity.Domain.Employer;
using Payeh.SharedKernel.EntityFrameworkCore.Domain;

namespace Nezam.Modular.ESS.Infrastructure.Data.Repository;

public class EmployerRepository : EntityFrameworkRepository<EmployerEntity, AppDbContext>, IEmployerRepository
{
    public EmployerRepository(AppDbContext context) : base(context)
    {
    }
}