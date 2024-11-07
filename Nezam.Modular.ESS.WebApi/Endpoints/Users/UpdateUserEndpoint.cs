using Bonyan.UserManagement.Domain.ValueObjects;
        using FastEndpoints;
        using Nezam.Modular.ESS.IdEntity.Application.Users;
        using Nezam.Modular.ESS.IdEntity.Application.Users.Dto;

        namespace Nezam.Modular.ESS.WebApi.Endpoints.Users;

        public class UpdateUserEndpoint : Endpoint< UserUpdateDto, UserDtoWithDetail>
        {
            private readonly IUserService userService;

            public UpdateUserEndpoint(IUserService userService)
            {
                this.userService = userService;
            }

            public override void Configure()
            {
                Put("/api/user/{BonUserId:guid}");

                Description(c =>
                {
                    c.WithTags("Users");
                });

                AllowAnonymous();
            }

            public override async Task HandleAsync(UserUpdateDto req, CancellationToken ct)
            {
                var updatedUser = await userService.UpdateUserAsync(BonUserId.FromGuid(req.BonUserId), req);
                if (updatedUser == null)
                {
                    await SendNotFoundAsync(ct);
                    return;
                }
                await SendOkAsync(updatedUser, ct);
            }
        }