public class RoleIdInputType : InputObjectType<Nezam.Modular.ESS.Identity.Domain.Shared.Roles.RoleId>
{
    protected override void Configure(IInputObjectTypeDescriptor<Nezam.Modular.ESS.Identity.Domain.Shared.Roles.RoleId> descriptor)
    {
        descriptor.Name("RoleIdInput");

        descriptor
            .Field("value")
            .Type<NonNullType<StringType>>();

        descriptor
            .Extend()
            .OnBeforeCreate((context, definition) =>
            {
                definition.RuntimeType = typeof(Nezam.Modular.ESS.Identity.Domain.Shared.Roles.RoleId);
            });

        descriptor.BindFieldsExplicitly();
    }
}