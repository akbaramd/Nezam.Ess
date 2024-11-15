using Bonyan.Layer.Domain.Specification.Abstractions;
using Nezam.Modular.ESS.Identity.Application.Roles.Dto;
using Nezam.Modular.ESS.Identity.Domain.Roles;

namespace Nezam.Modular.ESS.Identity.Application.Roles.Specs;

public class RolesFilterSpec : BonPaginatedSpecification<RoleEntity>
{
    public RolesFilterSpec(RoleFilterDto dto) : base(dto.Skip,dto.Take)
    {
    }

    public override void Handle(IBonSpecificationContext<RoleEntity> context)
    {
    }
}
