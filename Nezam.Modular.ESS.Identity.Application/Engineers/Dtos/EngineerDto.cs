using Bonyan.UserManagement.Domain.ValueObjects;
using Dto;
using FastEndpoints;
using Nezam.Modular.ESS.Identity.Domain.Shared.Engineer;

namespace Nezam.Modular.ESS.Identity.Application.Engineers.Dtos;

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
