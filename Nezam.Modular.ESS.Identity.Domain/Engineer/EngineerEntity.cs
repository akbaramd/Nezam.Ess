using Nezam.Modular.ESS.Identity.Domain.Shared.Employer;
using Nezam.Modular.ESS.Identity.Domain.Shared.User;
using Nezam.Modular.ESS.Identity.Domain.User;

namespace Nezam.Modular.ESS.Identity.Domain.Engineer
{
    public class EngineerEntity : UserEntity
    {
        protected EngineerEntity() { }
        public EngineerEntity( UserId userId,string registrationNumber, UserNameValue userName, UserPasswordValue password, UserProfileValue profile, UserEmailValue? email = null) : base(userId, userName, password, profile, email)
        {
            RegistrationNumber = registrationNumber;
        }
        public EmployerId EngineerId { get; set; }
        public string RegistrationNumber { get; private set; }
    
    }

    // Custom ID class for EngineerEntity
}
