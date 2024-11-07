using Bonyan.Layer.Domain;
using Nezam.Modular.ESS.IdEntity.Domain.Employer;

namespace Nezam.Modular.ESS.Infrastructure.Data.Repository;

public class EmployerRepository : EfCoreBonRepository<EmployerEntity,EmployerId, IdEntityDbContext>, IEmployerRepository
{
    public EmployerRepository(IdEntityDbContext userManagementDbContext) : base(userManagementDbContext)
    {
    }
}