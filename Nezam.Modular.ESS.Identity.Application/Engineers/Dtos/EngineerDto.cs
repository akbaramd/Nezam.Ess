using Bonyan.Layer.Application.Dto;
using FastEndpoints;
using Nezam.Modular.ESS.Identity.Application.Users.Dto;
using Nezam.Modular.ESS.Identity.Domain.Engineer;

namespace Nezam.Modular.ESS.Identity.Application.Engineers.Dtos;

public class EngineerDto : EntityDto<EngineerId>
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string RegistrationNumber { get; set; }
}

public class EngineerDtoWithDetails : EngineerDto
{
    public UserDto User { get; set; }
}

public class EngineerFilterDto 
{

    [QueryParam]
    public int Take { get; set; }
    [QueryParam]
    public int Skip { get; set; }
    
}
