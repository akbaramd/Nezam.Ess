using AutoMapper;
using Bonyan.Layer.Domain.Model;
using Nezam.Modular.ESS.IdEntity.Application.Employers.Dtos;
using Nezam.Modular.ESS.IdEntity.Domain.Employer;

namespace Nezam.Modular.ESS.IdEntity.Application.Employers;

public class EmployerProfile : Profile
{
    public EmployerProfile()
    {
        CreateMap<EmployerEntity,EmployerDto>();
        CreateMap<BonPaginatedResult<EmployerEntity>,BonPaginatedResult<EmployerDto>>();
    }
}
