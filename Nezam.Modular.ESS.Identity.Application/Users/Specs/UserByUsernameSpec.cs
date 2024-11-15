using Bonyan.Layer.Domain.Specification.Abstractions;
using Bonyan.Layer.Domain.Specifications;
using Nezam.Modular.ESS.Identity.Domain.User;

namespace Nezam.Modular.ESS.Identity.Application.Users.Specs;

public class UserByUsernameSpec : BonSpecification<UserEntity>
{
    private readonly string _username;

    public UserByUsernameSpec(string username)
    {
        _username = username;
    }

    public override void Handle(IBonSpecificationContext<UserEntity> context)
    {
        context.AddInclude(x => x.Roles);
        context.AddInclude(x => x.Employer);

        context.AddInclude(x => x.Engineer)
            ;

        context.AddCriteria(x => x.UserName.Equals(_username));
    }
}