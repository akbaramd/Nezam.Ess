using Bonyan.Layer.Application.Services;
using Bonyan.Layer.Domain.Model;
using Nezam.Modular.ESS.Identity.Application.Employers.Dtos;

namespace Nezam.Modular.ESS.Identity.Application.Employers;

public interface IEmployerService : IApplicationService
{
    Task<PaginatedResult<EmployerDto>> GetPaginatedResult(EmployerFilterDto filterDto);
}
