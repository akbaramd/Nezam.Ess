using Bonyan.Layer.Domain.Specifications;
using Nezam.Modular.ESS.IdEntity.Application.Employers.Dtos;
using Nezam.Modular.ESS.IdEntity.Domain.Employer;

namespace Nezam.Modular.ESS.IdEntity.Application.Employers.Specs;

public class EmployerFilterSpec : PaginatedSpecification<EmployerEntity>
{
    public EmployerFilterSpec(EmployerFilterDto dto) : base(dto.Skip,dto.Take)
    {
    }

    public override void Handle(ISpecificationContext<EmployerEntity> context)
    {
    }
}
