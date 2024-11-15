using Bonyan.UserManagement.Domain.ValueObjects;
using Dto;
using FastEndpoints;
using Nezam.Modular.ESS.Identity.Domain.Shared.Employer;

namespace Nezam.Modular.ESS.Identity.Application.Employers.Dtos;

public class EmployerDto : BonEntityDto<EmployerId>
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public BonUserId BonUserId { get; set; }
}

public class EmployerFilterDto 
{

    [QueryParam]
    public int Take { get; set; }
    [QueryParam]
    public int Skip { get; set; }
    
}
