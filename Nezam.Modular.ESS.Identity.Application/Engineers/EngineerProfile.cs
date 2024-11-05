using AutoMapper;
using Bonyan.Layer.Domain.Model;
using Nezam.Modular.ESS.Identity.Application.Engineers.Dtos;
using Nezam.Modular.ESS.Identity.Domain.Engineer;

namespace Nezam.Modular.ESS.Identity.Application.Engineers;

public class EngineerProfile : Profile
{
    public EngineerProfile()
    {
        CreateMap<EngineerEntity,EngineerDto>();
        CreateMap<EngineerEntity,EngineerDtoWithDetails>();
        CreateMap<PaginatedResult<EngineerEntity>,PaginatedResult<EngineerDto>>();
        CreateMap<PaginatedResult<EngineerEntity>,PaginatedResult<EngineerDtoWithDetails>>();
    }
}
