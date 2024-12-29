namespace Nezam.EES.Service.Identity.Application.UseCases.Users.CreateUser;

public record CreateUserRecord(string UserName,string Password,string FirstName,string LastName,string Email);