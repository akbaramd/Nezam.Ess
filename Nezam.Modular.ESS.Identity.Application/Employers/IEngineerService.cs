using Bonyan.Layer.Application.Services;
using Bonyan.Layer.Domain.Model;
using Nezam.Modular.ESS.IdEntity.Application.Employers.Dtos;
using Nezam.Modular.ESS.IdEntity.Domain.Employer;

namespace Nezam.Modular.ESS.IdEntity.Application.Employers;

public interface IEmployerService : IBonApplicationService
{
    Task<BonPaginatedResult<EmployerDto>> GetBonPaginatedResult(EmployerFilterDto filterDto);
    Task<EmployerDto> GetEmployerByIdAsync(EmployerId employerId);
    Task<EmployerDto> AddEmployerAsync(EmployerCreateDto createDto);
    Task<EmployerDto> UpdateEmployerAsync(EmployerId employerId, EmployerUpdateDto updateDto);
    Task<bool> DeleteEmployerAsync(EmployerId employerId);
}