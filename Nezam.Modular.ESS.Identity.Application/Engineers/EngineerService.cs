using Bonyan.Layer.Application.Services;
using Bonyan.Layer.Domain.Model;
using Nezam.Modular.ESS.Identity.Application.Engineers.Dtos;
using Nezam.Modular.ESS.Identity.Application.Engineers.Specs;
using Nezam.Modular.ESS.Identity.Domain.Engineer;

namespace Nezam.Modular.ESS.Identity.Application.Engineers;

public class EngineerService : ApplicationService, IEngineerService
{
    public IEngineerRepository EngineerRepository => LazyServiceProvider.LazyGetRequiredService<IEngineerRepository>();

    public async Task<PaginatedResult<EngineerDtoWithDetails>> GetPaginatedResult(EngineerFilterDto filterDto)
    {
        var res = await EngineerRepository.PaginatedAsync(new EngineerFilterSpec(filterDto));
        return Mapper.Map<PaginatedResult<EngineerEntity>,PaginatedResult<EngineerDtoWithDetails>>(res);
    }

   
}
