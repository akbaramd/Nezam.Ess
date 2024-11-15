using Bonyan.Layer.Domain.Specification.Abstractions;
using Nezam.Modular.ESS.Identity.Application.Engineers.Dtos;
using Nezam.Modular.ESS.Identity.Domain.Engineer;

namespace Nezam.Modular.ESS.Identity.Application.Engineers.Specs;

public class EngineerFilterSpec : BonPaginatedSpecification<EngineerEntity>
{
    public EngineerFilterSpec(EngineerFilterDto dto) : base(dto.Skip,dto.Take)
    {
    }

    public override void Handle(IBonSpecificationContext<EngineerEntity> context)
    {
    }
}
