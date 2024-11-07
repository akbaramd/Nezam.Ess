using AutoMapper;
using Bonyan.Layer.Domain.Model;
using Nezam.Modular.ESS.IdEntity.Application.Engineers.Dtos;
using Nezam.Modular.ESS.IdEntity.Domain.Engineer;
using EngineerDto = Nezam.Modular.ESS.Identity.Application.Engineers.Jobs.EngineerDto;

namespace Nezam.Modular.ESS.IdEntity.Application.Engineers;

public class EngineerProfile : Profile
{
    public EngineerProfile()
    {
        CreateMap<EngineerEntity,EngineerDto>();
        CreateMap<EngineerEntity,EngineerDtoWithDetails>();
        CreateMap<BonPaginatedResult<EngineerEntity>,BonPaginatedResult<EngineerDto>>();
        CreateMap<BonPaginatedResult<EngineerEntity>,BonPaginatedResult<EngineerDtoWithDetails>>();
    }
}
