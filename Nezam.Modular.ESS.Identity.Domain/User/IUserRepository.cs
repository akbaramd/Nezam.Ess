using Bonyan.Layer.Domain.Repository.Abstractions;
using Bonyan.UserManagement.Domain.ValueObjects;

namespace Nezam.Modular.ESS.Identity.Domain.User;

public interface IUserRepository : IBonRepository<UserEntity,BonUserId>
{
   
}