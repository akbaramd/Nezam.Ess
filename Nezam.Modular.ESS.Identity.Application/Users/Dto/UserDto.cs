using Bonyan.UserManagement.Application.Dtos;
using FastEndpoints;
using Microsoft.AspNetCore.Mvc;
using Nezam.Modular.ESS.IdEntity.Application.Employers.Dtos;
using Nezam.Modular.ESS.IdEntity.Application.Roles.Dto;

namespace Nezam.Modular.ESS.IdEntity.Application.Users.Dto;

public class UserDto : BonUserDto
{
    public IReadOnlyCollection<RoleDto> Roles { get; set; }

}

public class UserDtoWithDetail : UserDto
{

    public EmployerDto? Employer { get; set; }
    public Engineers.Dtos.EngineerDto? Engineer { get; set; }
}
public class UserFilterDto 
{

    [QueryParam]
    public int Take { get; set; }
    [QueryParam]
    public int Skip { get; set; }

    [QueryParam]
    public string? Search { get; set; }
    
    [QueryParam]
    public string SortBy { get; set; }
    
    [QueryParam]
    public string SortDirection { get; set; }
    
    
}

public class UserUpdateDto 
{
    [FromRoute]
    public Guid BonUserId { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
    public string[] Roles { get; set; }
}

