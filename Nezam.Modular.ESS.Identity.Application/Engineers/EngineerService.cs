using Bonyan.Layer.Application.Services;
using Bonyan.Layer.Domain.Repository.Abstractions;
using Nezam.Modular.ESS.Identity.Application.Engineers.Dtos;
using Nezam.Modular.ESS.Identity.Application.Engineers.Specs;
using Nezam.Modular.ESS.Identity.Domain.Engineer;

namespace Nezam.Modular.ESS.Identity.Application.Engineers;

public class EngineerService : BonApplicationService, IEngineerService
{
    public IEngineerRepository EngineerRepository => LazyServiceProvider.LazyGetRequiredService<IEngineerRepository>();

    public async Task<BonPaginatedResult<EngineerDtoWithDetails>> GetBonPaginatedResult(EngineerFilterDto filterDto)
    {
        var res = await EngineerRepository.PaginatedAsync(new EngineerFilterSpec(filterDto));
        return Mapper.Map<BonPaginatedResult<EngineerEntity>,BonPaginatedResult<EngineerDtoWithDetails>>(res);
    }

   
}
