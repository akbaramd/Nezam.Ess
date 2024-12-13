public class UserIdInputType : InputObjectType<Nezam.Modular.ESS.Identity.Domain.Shared.User.UserId>
{
    protected override void Configure(IInputObjectTypeDescriptor<Nezam.Modular.ESS.Identity.Domain.Shared.User.UserId> descriptor)
    {
        descriptor.Name("UserIdInput");

        descriptor
            .Field("value")
            .Type<NonNullType<StringType>>();

        descriptor
            .Extend()
            .OnBeforeCreate((context, definition) =>
            {
                definition.RuntimeType = typeof(Nezam.Modular.ESS.Identity.Domain.Shared.User.UserId);
            });

        descriptor.BindFieldsExplicitly();
    }
}