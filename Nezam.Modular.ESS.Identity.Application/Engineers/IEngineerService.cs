using Bonyan.Layer.Application.Services;
using Bonyan.Layer.Domain.Model;
using Nezam.Modular.ESS.Identity.Application.Engineers.Dtos;

namespace Nezam.Modular.ESS.Identity.Application.Engineers;

public interface IEngineerService : IBonApplicationService
{
    Task<BonPaginatedResult<EngineerDtoWithDetails>> GetBonPaginatedResult(EngineerFilterDto filterDto);
}
