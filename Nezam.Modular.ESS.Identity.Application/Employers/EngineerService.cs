using Bonyan.Layer.Application.Services;
using Bonyan.Layer.Domain.Model;
using Nezam.Modular.ESS.IdEntity.Application.Employers.Dtos;
using Nezam.Modular.ESS.IdEntity.Application.Employers.Specs;
using Nezam.Modular.ESS.IdEntity.Domain.Employer;

namespace Nezam.Modular.ESS.IdEntity.Application.Employers
{
    public class EmployerService : BonApplicationService, IEmployerService
    {
        public IEmployerRepository EmployerRepository => LazyServiceProvider.LazyGetRequiredService<IEmployerRepository>();

        // Method to get a paginated list of employers based on filters
        public async Task<BonPaginatedResult<EmployerDto>> GetBonPaginatedResult(EmployerFilterDto filterDto)
        {
            var res = await EmployerRepository.PaginatedAsync(new EmployerFilterSpec(filterDto));
            return Mapper.Map<BonPaginatedResult<EmployerEntity>, BonPaginatedResult<EmployerDto>>(res);
        }

        // Method to retrieve an employer by its unique identifier
        public async Task<EmployerDto> GetEmployerByIdAsync(EmployerId employerId)
        {
            var employer = await EmployerRepository.GetByIdAsync(employerId);
            if (employer == null)
                throw new KeyNotFoundException($"Employer with ID {employerId} not found.");

            return Mapper.Map<EmployerDto>(employer);
        }

        public Task<EmployerDto> AddEmployerAsync(EmployerCreateDto createDto)
        {
            throw new NotImplementedException();
        }

        public Task<EmployerDto> UpdateEmployerAsync(EmployerId employerId, EmployerUpdateDto updateDto)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteEmployerAsync(EmployerId employerId)
        {
            throw new NotImplementedException();
        }
    }
}
