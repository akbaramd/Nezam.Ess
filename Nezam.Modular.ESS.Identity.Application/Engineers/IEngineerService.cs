using Bonyan.Layer.Application.Services;
using Bonyan.Layer.Domain.Model;
using Nezam.Modular.ESS.IdEntity.Application.Engineers.Dtos;

namespace Nezam.Modular.ESS.IdEntity.Application.Engineers;

public interface IEngineerService : IBonApplicationService
{
    Task<BonPaginatedResult<EngineerDtoWithDetails>> GetBonPaginatedResult(EngineerFilterDto filterDto);
}
