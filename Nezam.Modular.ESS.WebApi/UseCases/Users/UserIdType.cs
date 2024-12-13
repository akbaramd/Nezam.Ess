using HotChocolate.Types;
using Nezam.Modular.ESS.Identity.Domain.Shared.User;

public class UserIdType : ObjectType<UserId>
{
    protected override void Configure(IObjectTypeDescriptor<UserId> descriptor)
    {
        descriptor.Field(x => x.Value).Type<NonNullType<StringType>>();
    }
}