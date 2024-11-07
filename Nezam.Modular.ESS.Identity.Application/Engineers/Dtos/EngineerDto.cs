using Bonyan.Layer.Application.Dto;
using Bonyan.UserManagement.Domain.ValueObjects;
using FastEndpoints;
using Nezam.Modular.ESS.IdEntity.Application.Users.Dto;
using Nezam.Modular.ESS.IdEntity.Domain.Engineer;

namespace Nezam.Modular.ESS.IdEntity.Application.Engineers.Dtos;

public class EngineerDto : BonEntityDto<EngineerId>
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string RegistrationNumber { get; set; }
}

public class EngineerDtoWithDetails : EngineerDto
{
    public BonUserId BonUserId { get; set; }
}

public class EngineerFilterDto 
{

    [QueryParam]
    public int Take { get; set; }
    [QueryParam]
    public int Skip { get; set; }
    
}
