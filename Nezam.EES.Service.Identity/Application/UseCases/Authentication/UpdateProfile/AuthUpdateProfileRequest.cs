using Nezam.EEs.Shared.Domain.Identity.User.ValueObjects;

namespace Nezam.EES.Service.Identity.Application.UseCases.Authentication.UpdateProfile;

public class AuthUpdateProfileRequest
{

    [FastEndpoints.FromBody]
    public UserProfileValue Profile { get; set; } = default!; // Profile info
}