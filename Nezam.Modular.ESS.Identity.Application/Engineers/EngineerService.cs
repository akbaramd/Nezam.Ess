using Bonyan.Layer.Application.Services;
using Bonyan.Layer.Domain.Model;
using Nezam.Modular.ESS.IdEntity.Application.Engineers.Dtos;
using Nezam.Modular.ESS.IdEntity.Application.Engineers.Specs;
using Nezam.Modular.ESS.IdEntity.Domain.Engineer;

namespace Nezam.Modular.ESS.IdEntity.Application.Engineers;

public class EngineerService : BonApplicationService, IEngineerService
{
    public IEngineerRepository EngineerRepository => LazyServiceProvider.LazyGetRequiredService<IEngineerRepository>();

    public async Task<BonPaginatedResult<EngineerDtoWithDetails>> GetBonPaginatedResult(EngineerFilterDto filterDto)
    {
        var res = await EngineerRepository.PaginatedAsync(new EngineerFilterSpec(filterDto));
        return Mapper.Map<BonPaginatedResult<EngineerEntity>,BonPaginatedResult<EngineerDtoWithDetails>>(res);
    }

   
}
