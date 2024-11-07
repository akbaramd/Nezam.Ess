using Bonyan.Layer.Domain.Specifications;
using Nezam.Modular.ESS.IdEntity.Domain.User;

namespace Nezam.Modular.ESS.IdEntity.Application.Users.Specs;

public class UserByUsernameSpec : Specification<UserEntity>
{
    private readonly string _username;

    public UserByUsernameSpec(string username)
    {
        _username = username;
    }

    public override void Handle(ISpecificationContext<UserEntity> context)
    {
        context.AddInclude(x => x.Roles);
        context.AddInclude(x => x.Employer);

        context.AddInclude(x => x.Engineer)
            ;

        context.AddCriteria(x => x.UserName.Equals(_username));
    }
}