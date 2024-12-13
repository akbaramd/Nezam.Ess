using Nezam.Modular.ESS.Identity.Domain.Shared.Employer;
using Nezam.Modular.ESS.Identity.Domain.Shared.User;
using Nezam.Modular.ESS.Identity.Domain.User;

namespace Nezam.Modular.ESS.Identity.Domain.Employer
{
    public class EmployerEntity : UserEntity
    {
        public EmployerId EmployerId { get; set; }
        protected EmployerEntity() { }
        public EmployerEntity(UserId userId, UserNameValue userName, UserPasswordValue password, UserProfileValue? profile=null, UserEmailValue? email = null) : base( userId,userName, password, profile, email)
        {
            EmployerId = EmployerId.NewId();
        }
    }

    // Custom ID class for EmployerEntity
}