using Bonyan.Layer.Domain.Specifications;
using Nezam.Modular.ESS.Identity.Application.Engineers.Dtos;
using Nezam.Modular.ESS.Identity.Domain.Engineer;

namespace Nezam.Modular.ESS.Identity.Application.Engineers.Specs;

public class EngineerFilterSpec : PaginatedSpecification<EngineerEntity>
{
    public EngineerFilterSpec(EngineerFilterDto dto) : base(dto.Skip,dto.Take)
    {
    }

    public override void Handle(ISpecificationContext<EngineerEntity> context)
    {
    }
}
