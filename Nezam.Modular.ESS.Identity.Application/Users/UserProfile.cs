using AutoMapper;
using Bonyan.Layer.Domain.Model;
using Nezam.Modular.ESS.IdEntity.Application.Users.Dto;
using Nezam.Modular.ESS.IdEntity.Domain.User;

namespace Nezam.Modular.ESS.IdEntity.Application.Users;

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
