using System;
using Bonyan.Layer.Application.Dto;
using FastEndpoints;
using Nezam.Modular.ESS.Identity.Domain.Roles;

namespace Nezam.Modular.ESS.Identity.Application.Roles.Dto;

public class RoleDto : EntityDto<RoleId>
{
        public string Name { get; set; }
    public string Title { get; set; }
}
public class RoleFilterDto 
{

    [QueryParam]
    public int Take { get; set; }
    [QueryParam]
    public int Skip { get; set; }
}
