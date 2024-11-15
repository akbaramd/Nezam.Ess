using AutoMapper;
using Bonyan.Layer.Domain.Repository.Abstractions;
using Nezam.Modular.ESS.Identity.Application.Users.Dto;
using Nezam.Modular.ESS.Identity.Domain.User;

namespace Nezam.Modular.ESS.Identity.Application.Users;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<UserEntity,UserDto>();
        CreateMap<UserEntity,UserDtoWithDetail>();
        
        CreateMap<BonPaginatedResult<UserEntity>,BonPaginatedResult<UserDto>>();
        CreateMap<BonPaginatedResult<UserEntity>,BonPaginatedResult<UserDtoWithDetail>>();
    }
}
