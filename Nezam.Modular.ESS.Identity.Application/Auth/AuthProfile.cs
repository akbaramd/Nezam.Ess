using AutoMapper;
using Bonyan.UserManagement.Application.Dtos;
using Nezam.Modular.ESS.IdEntity.Domain.User;

namespace Nezam.Modular.ESS.IdEntity.Application.Auth;

public class AuthProfile : Profile
{
    public AuthProfile()
    {
        CreateMap<UserEntity, BonUserDto>();
    }
}