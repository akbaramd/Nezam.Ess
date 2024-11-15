using Bonyan.Layer.Domain.Specification.Abstractions;
using Nezam.Modular.ESS.Identity.Application.Employers.Dtos;
using Nezam.Modular.ESS.Identity.Domain.Employer;

namespace Nezam.Modular.ESS.Identity.Application.Employers.Specs;

public class EmployerFilterSpec : BonPaginatedSpecification<EmployerEntity>
{
    public EmployerFilterSpec(EmployerFilterDto dto) : base(dto.Skip,dto.Take)
    {
    }

    public override void Handle(IBonSpecificationContext<EmployerEntity> context)
    {
    }
}
