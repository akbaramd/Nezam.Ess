using Bonyan.Layer.Application.Services;
using Bonyan.Layer.Domain.Model;
using Nezam.Modular.ESS.Identity.Application.Employers.Dtos;
using Nezam.Modular.ESS.Identity.Domain.Employer;

namespace Nezam.Modular.ESS.Identity.Application.Employers;

public interface IEmployerService : IApplicationService
{
    Task<PaginatedResult<EmployerDto>> GetPaginatedResult(EmployerFilterDto filterDto);
    Task<EmployerDto> GetEmployerByIdAsync(EmployerId employerId);
    Task<EmployerDto> AddEmployerAsync(EmployerCreateDto createDto);
    Task<EmployerDto> UpdateEmployerAsync(EmployerId employerId, EmployerUpdateDto updateDto);
    Task<bool> DeleteEmployerAsync(EmployerId employerId);
}