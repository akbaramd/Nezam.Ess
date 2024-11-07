using Bonyan.Layer.Domain.Abstractions;
using Bonyan.UserManagement.Domain.ValueObjects;

namespace Nezam.Modular.ESS.IdEntity.Domain.User;

public interface IUserRepository : IBonRepository<UserEntity,BonUserId>
{
   
}