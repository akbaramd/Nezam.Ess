using Bonyan.Layer.Application.Services;
using Bonyan.Layer.Domain.Model;
using Nezam.Modular.ESS.Identity.Application.Employers.Dtos;
using Nezam.Modular.ESS.Identity.Application.Employers.Specs;
using Nezam.Modular.ESS.Identity.Domain.Employer;

namespace Nezam.Modular.ESS.Identity.Application.Employers;

public class EmployerService : ApplicationService, IEmployerService
{
    public IEmployerRepository EmployerRepository => LazyServiceProvider.LazyGetRequiredService<IEmployerRepository>();

    public async Task<PaginatedResult<EmployerDto>> GetPaginatedResult(EmployerFilterDto filterDto)
    {
        var res = await EmployerRepository.PaginatedAsync(new EmployerFilterSpec(filterDto));
        return Mapper.Map<PaginatedResult<EmployerEntity>,PaginatedResult<EmployerDto>>(res);
    }

   
}
