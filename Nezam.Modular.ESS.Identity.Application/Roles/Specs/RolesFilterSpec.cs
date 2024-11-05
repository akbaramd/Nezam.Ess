using System;
using Bonyan.Layer.Domain.Specifications;
using Nezam.Modular.ESS.Identity.Application.Roles.Dto;
using Nezam.Modular.ESS.Identity.Domain.Roles;

namespace Nezam.Modular.ESS.Identity.Application.Roles.Specs;

public class RolesFilterSpec : PaginatedSpecification<RoleEntity>
{
    public RolesFilterSpec(RoleFilterDto dto) : base(dto.Skip,dto.Take)
    {
    }

    public override void Handle(ISpecificationContext<RoleEntity> context)
    {
    }
}
