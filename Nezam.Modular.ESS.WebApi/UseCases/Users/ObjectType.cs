using HotChocolate.Types;
using Nezam.Modular.ESS.Identity.Domain.User;

public class UserType : ObjectType<UserEntity>
{
    protected override void Configure(IObjectTypeDescriptor<UserEntity> descriptor)
    {
        descriptor.Field(x => x.DomainEvents).Ignore(); // Ignore domain events in the schema
        descriptor.Field(x => x.Password).Ignore(); // Ignore domain events in the schema
        descriptor.Field(x => x.AddRole).Ignore(); // Ignore domain events in the schema
    }
}