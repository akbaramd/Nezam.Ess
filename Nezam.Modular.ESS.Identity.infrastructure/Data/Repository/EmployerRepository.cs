using Bonyan.Layer.Domain;
using Nezam.Modular.ESS.Identity.Domain.Employer;
using Nezam.Modular.ESS.Identity.Domain.Roles;

namespace Nezam.Modular.ESS.Identity.infrastructure.Data.Repository;

public class EmployerRepository : EfCoreRepository<EmployerEntity,EmployerId, IdentityDbContext>, IEmployerRepository
{
    public EmployerRepository(IdentityDbContext userManagementDbContext) : base(userManagementDbContext)
    {
    }
}