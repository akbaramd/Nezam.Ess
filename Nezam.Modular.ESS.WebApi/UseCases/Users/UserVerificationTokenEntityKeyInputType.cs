using HotChocolate.Types;

public class UserVerificationTokenEntityKeyInputType : InputObjectType<Nezam.Modular.ESS.Identity.Domain.User.UserVerificationTokenEntityKey>
{
    protected override void Configure(IInputObjectTypeDescriptor<Nezam.Modular.ESS.Identity.Domain.User.UserVerificationTokenEntityKey> descriptor)
    {
        descriptor.Name("UserVerificationTokenEntityKeyInput");

        descriptor
            .Field("value")
            .Type<NonNullType<StringType>>();

        descriptor
            .Extend()
            .OnBeforeCreate((context, definition) =>
            {
                definition.RuntimeType = typeof(Nezam.Modular.ESS.Identity.Domain.User.UserVerificationTokenEntityKey);
            });

        descriptor.BindFieldsExplicitly();
    }
}