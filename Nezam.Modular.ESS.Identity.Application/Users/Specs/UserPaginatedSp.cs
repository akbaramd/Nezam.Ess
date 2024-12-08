using Bonyan.Layer.Domain.Specifications;
using Nezam.Modular.ESS.Identity.Application.Users.Dto;
using Nezam.Modular.ESS.Identity.Domain.User;

namespace Nezam.Modular.ESS.Identity.Application.Users.Specs;

public class UsersFilterSpec : PaginatedAndSortableSpecification<UserEntity>
{
    private readonly UserFilterDto _dto;

    public UsersFilterSpec(UserFilterDto dto) : base(dto.Skip,dto.Take,dto.SortBy,dto.SortDirection)
    {
        _dto = dto;
    }

    public override void Handle(ISpecificationContext<UserEntity> context)
    {
        context
            .AddInclude(x => x.Roles)
            .AddInclude(x => x.Employer)
            .AddInclude(x => x.Engineer);

            if (!string.IsNullOrWhiteSpace(_dto.Search))
            {
                context.AddCriteria(x=>x.Email != null && x.PhoneNumber != null && (x.UserName.Contains(_dto.Search) 
                    || x.Email.Address.Contains(_dto.Search) 
                    || x.PhoneNumber.Number.Contains(_dto.Search)));
            }
    }
}
