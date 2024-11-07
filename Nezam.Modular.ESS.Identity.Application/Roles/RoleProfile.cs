using AutoMapper;
using Bonyan.Layer.Domain.Model;
using Nezam.Modular.ESS.IdEntity.Application.Roles.Dto;
using Nezam.Modular.ESS.IdEntity.Domain.Roles;

namespace Nezam.Modular.ESS.IdEntity.Application.Roles;

public class RoleProfile : Profile
{
    public RoleProfile()
    {
        CreateMap<RoleEntity,RoleDto>();
        CreateMap<BonPaginatedResult<RoleEntity>,BonPaginatedResult<RoleDto>>();
    }
}
