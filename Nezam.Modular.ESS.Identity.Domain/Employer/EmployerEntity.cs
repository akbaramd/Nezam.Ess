using Bonyan.Layer.Domain.Entities;
using Bonyan.Layer.Domain.ValueObjects;
using Bonyan.UserManagement.Domain.ValueObjects;
using Nezam.Modular.ESS.Identity.Domain.User;

namespace Nezam.Modular.ESS.Identity.Domain.Employer;

public class EmployerEntity : Entity<EmployerId>
{
    public UserEntity User { get; set; }
    public UserId UserId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    
    protected EmployerEntity(){}

    public EmployerEntity(UserEntity user, string firstName , string lastName)
    {
        FirstName = firstName;
        LastName = lastName;
        User = user;
        UserId = user.Id;
    }
}

public class EmployerId : BusinessId<EmployerId>
{
}