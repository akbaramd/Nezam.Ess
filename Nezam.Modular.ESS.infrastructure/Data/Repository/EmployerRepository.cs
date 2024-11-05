using Bonyan.Layer.Domain;
using Nezam.Modular.ESS.Identity.Domain.Employer;

namespace Nezam.Modular.ESS.infrastructure.Data.Repository;

public class EmployerRepository : EfCoreRepository<EmployerEntity,EmployerId, IdentityDbContext>, IEmployerRepository
{
    public EmployerRepository(IdentityDbContext userManagementDbContext) : base(userManagementDbContext)
    {
    }
}