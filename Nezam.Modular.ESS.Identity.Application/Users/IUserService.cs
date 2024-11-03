using System;
using Bonyan.Layer.Application.Services;
using Bonyan.Layer.Domain.Model;
using Nezam.Modular.ESS.Identity.Application.Users.Dto;

namespace Nezam.Modular.ESS.Identity.Application.Users;

public interface IUserService : IApplicationService
{
    Task<PaginatedResult<UserDtoWithDetail>> GetPaginatedResult(UserFilterDto filterDto);
}
