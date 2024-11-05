using System;
using AutoMapper;
using Bonyan.Layer.Domain.Model;
using Nezam.Modular.ESS.Identity.Application.Users.Dto;
using Nezam.Modular.ESS.Identity.Domain.User;

namespace Nezam.Modular.ESS.Identity.Application.Users;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<UserEntity,UserDto>();
        CreateMap<UserEntity,UserDtoWithDetail>();
        
        CreateMap<PaginatedResult<UserEntity>,PaginatedResult<UserDto>>();
        CreateMap<PaginatedResult<UserEntity>,PaginatedResult<UserDtoWithDetail>>();
    }
}
