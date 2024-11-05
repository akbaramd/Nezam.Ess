using System;
using Bonyan.UserManagement.Application.Dtos;
using Bonyan.UserManagement.Domain.ValueObjects;
using FastEndpoints;
using Microsoft.AspNetCore.Mvc;
using Nezam.Modular.ESS.Identity.Application.Employers.Dtos;
using Nezam.Modular.ESS.Identity.Application.Roles.Dto;
using EngineerDto = Nezam.Modular.ESS.Identity.Application.Engineers.Jobs.EngineerDto;

namespace Nezam.Modular.ESS.Identity.Application.Users.Dto;

public class UserDto : BonyanUserDto
{
    public IReadOnlyCollection<RoleDto> Roles { get; set; }

}

public class UserDtoWithDetail : UserDto
{

    public EmployerDto? Employer { get; set; }
    public EngineerDto? Engineer { get; set; }
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
    public Guid UserId { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
    public string[] Roles { get; set; }
}

