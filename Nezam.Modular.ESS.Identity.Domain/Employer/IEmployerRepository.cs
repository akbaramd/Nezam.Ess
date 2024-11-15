using Bonyan.Layer.Domain.Repository.Abstractions;
using Nezam.Modular.ESS.Identity.Domain.Shared.Employer;

namespace Nezam.Modular.ESS.Identity.Domain.Employer;

public interface IEmployerRepository : IBonRepository<EmployerEntity,EmployerId>
{
    
}