using Bonyan.Layer.Domain.Abstractions;

namespace Nezam.Modular.ESS.Identity.Domain.User;

public interface IUserVerificationTokenRepository : IBonRepository<UserVerificationTokenEntity>
{
    
}