using System;
using Bonyan.Layer.Application.Services;
using Bonyan.Layer.Domain.Abstractions;
using Bonyan.Layer.Domain.Model;
using Nezam.Modular.ESS.Identity.Application.Users.Dto;
using Nezam.Modular.ESS.Identity.Application.Users.Specs;
using Nezam.Modular.ESS.Identity.Domain.User;

namespace Nezam.Modular.ESS.Identity.Application.Users;

public class UserService : ApplicationService, IUserService
{
    public IUserRepository UserRepository => LazyServiceProvider.LazyGetRequiredService<IUserRepository>();

    public async Task<PaginatedResult<UserDtoWithDetail>> GetPaginatedResult(UserFilterDto filterDto)
    {
        var res = await UserRepository.PaginatedAsync(new UsersFilterSpec(filterDto));
        return Mapper.Map<PaginatedResult<UserEntity>,PaginatedResult<UserDtoWithDetail>>(res);
    }

   
}
