using AutoMapper;
using Bonyan.Layer.Domain.Model;
using Nezam.Modular.ESS.Identity.Application.Employers.Dtos;
using Nezam.Modular.ESS.Identity.Domain.Employer;

namespace Nezam.Modular.ESS.Identity.Application.Employers;

public class EmployerProfile : Profile
{
    public EmployerProfile()
    {
        CreateMap<EmployerEntity,EmployerDto>();
        CreateMap<BonPaginatedResult<EmployerEntity>,BonPaginatedResult<EmployerDto>>();
    }
}
