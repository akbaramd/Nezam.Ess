using System;
using Bonyan.Layer.Domain.Specifications;
using Bonyan.UserManagement.Domain.ValueObjects;
using Nezam.Modular.ESS.Identity.Application.Users.Dto;
using Nezam.Modular.ESS.Identity.Domain.User;

namespace Nezam.Modular.ESS.Identity.Application.Users.Specs;

public class UserByIdSpec : Specification<UserEntity>
{
    private readonly UserId _userId;

    public UserByIdSpec(UserId id)
    {
        _userId = id;
    }

    public override void Handle(ISpecificationContext<UserEntity> context)
    {
        context.AddInclude(x => x.Roles);
        context.AddInclude(x => x.Employer);

        context.AddInclude(x => x.Engineer)
            ;

        context.AddCriteria(x => x.Id.Equals(_userId));
    }
}