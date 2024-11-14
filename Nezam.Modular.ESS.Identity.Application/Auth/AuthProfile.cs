using AutoMapper;
using Bonyan.UserManagement.Application.Dtos;
using Nezam.Modular.ESS.Identity.Domain.User;

namespace Nezam.Modular.ESS.Identity.Application.Auth;

public class AuthProfile : Profile
{
    public AuthProfile()
    {
        CreateMap<UserEntity, BonUserDto>();
    }
}