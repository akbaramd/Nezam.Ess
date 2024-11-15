using Bonyan.Layer.Domain.Specification.Abstractions;
using Bonyan.Layer.Domain.Specifications;
using Bonyan.UserManagement.Domain.ValueObjects;
using Nezam.Modular.ESS.Identity.Domain.User;

namespace Nezam.Modular.ESS.Identity.Application.Users.Specs;

public class UserByIdSpec : BonSpecification<UserEntity>
{
    private readonly BonUserId _BonUserId;

    public UserByIdSpec(BonUserId id)
    {
        _BonUserId = id;
    }

    public override void Handle(IBonSpecificationContext<UserEntity> context)
    {
        context.AddInclude(x => x.Roles);
        context.AddInclude(x => x.Employer);

        context.AddInclude(x => x.Engineer)
            ;

        context.AddCriteria(x => x.Id.Equals(_BonUserId));
    }
}