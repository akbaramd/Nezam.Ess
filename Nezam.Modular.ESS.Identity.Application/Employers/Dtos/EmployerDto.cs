using Bonyan.Layer.Application.Dto;
using FastEndpoints;
using Nezam.Modular.ESS.Identity.Domain.Employer;

namespace Nezam.Modular.ESS.Identity.Application.Employers.Dtos;

public class EmployerDto : EntityDto<EmployerId>
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
}

public class EmployerFilterDto 
{

    [QueryParam]
    public int Take { get; set; }
    [QueryParam]
    public int Skip { get; set; }
    
}
