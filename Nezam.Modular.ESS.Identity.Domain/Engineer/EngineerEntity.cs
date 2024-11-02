using Bonyan.Layer.Domain.Entities;
using Bonyan.Layer.Domain.ValueObjects;
using Bonyan.UserManagement.Domain.ValueObjects;
using Nezam.Modular.ESS.Identity.Domain.User;

namespace Nezam.Modular.ESS.Identity.Domain.Engineer;

public class EngineerEntity : Entity<EngineerId>
{
    protected EngineerEntity(){}

    public EngineerEntity(EngineerId engineerId,UserEntity user, string? firstName, string? lastName, string registrationNumber)
    {
        Id = engineerId;
        User = user;
        UserId = user.Id;
        FirstName = firstName;
        LastName = lastName;
        RegistrationNumber = registrationNumber;
    }

    public UserEntity User { get; set; } 
    public UserId UserId { get; set; } 
    public string? FirstName { get; set; }
    public string? LastName { get; set; } 
    public string RegistrationNumber { get; set; }
}   

public class EngineerId : BusinessId<EngineerId>
{
}