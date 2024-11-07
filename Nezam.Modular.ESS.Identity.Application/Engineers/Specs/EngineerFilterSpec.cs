using Bonyan.Layer.Domain.Specifications;
using Nezam.Modular.ESS.IdEntity.Application.Engineers.Dtos;
using Nezam.Modular.ESS.IdEntity.Domain.Engineer;

namespace Nezam.Modular.ESS.IdEntity.Application.Engineers.Specs;

public class EngineerFilterSpec : PaginatedSpecification<EngineerEntity>
{
    public EngineerFilterSpec(EngineerFilterDto dto) : base(dto.Skip,dto.Take)
    {
    }

    public override void Handle(ISpecificationContext<EngineerEntity> context)
    {
    }
}
