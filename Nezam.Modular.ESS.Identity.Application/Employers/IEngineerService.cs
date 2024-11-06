using Bonyan.Layer.Application.Services;
using Bonyan.Layer.Domain.Model;
using Nezam.Modular.ESS.Identity.Application.Employers.Dtos;

namespace Nezam.Modular.ESS.Identity.Application.Employers;

public interface IEmployerService : IApplicationService
{
    Task<PaginatedResult<EmployerDto>> GetPaginatedResult(EmployerFilterDto filterDto);
    Task<EmployerDto> GetEmployerByIdAsync(Guid employerId);
    Task<EmployerDto> AddEmployerAsync(EmployerCreateDto createDto);
    Task<EmployerDto> UpdateEmployerAsync(Guid employerId, EmployerUpdateDto updateDto);
    Task<bool> DeleteEmployerAsync(Guid employerId);
}