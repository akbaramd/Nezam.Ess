using Bonyan.Layer.Domain.Specifications;
using Nezam.Modular.ESS.Identity.Application.Employers.Dtos;
using Nezam.Modular.ESS.Identity.Application.Users.Dto;
using Nezam.Modular.ESS.Identity.Domain.Employer;
using Nezam.Modular.ESS.Identity.Domain.Engineer;

namespace Nezam.Modular.ESS.Identity.Application.Employers.Specs;

public class EmployerFilterSpec : PaginatedSpecification<EmployerEntity>
{
    public EmployerFilterSpec(EmployerFilterDto dto) : base(dto.Skip,dto.Take)
    {
    }

    public override void Handle(ISpecificationContext<EmployerEntity> context)
    {
        context
            .AddInclude(x => x.User);
    }
}
