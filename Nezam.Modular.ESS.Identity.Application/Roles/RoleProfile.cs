using AutoMapper;
using Bonyan.Layer.Domain.Repository.Abstractions;
using Nezam.Modular.ESS.Identity.Application.Roles.Dto;
using Nezam.Modular.ESS.Identity.Domain.Roles;

namespace Nezam.Modular.ESS.Identity.Application.Roles;

public class RoleProfile : Profile
{
    public RoleProfile()
    {
        CreateMap<RoleEntity,RoleDto>();
        CreateMap<BonPaginatedResult<RoleEntity>,BonPaginatedResult<RoleDto>>();
    }
}
