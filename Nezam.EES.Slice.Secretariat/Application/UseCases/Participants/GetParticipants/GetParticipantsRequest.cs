using System.Security.Claims;
using FastEndpoints;

namespace Nezam.EES.Slice.Secretariat.Application.UseCases.Participants.GetParticipants;

public class GetParticipantsRequest
{
    [FromClaim(ClaimTypes.NameIdentifier)]
    public Guid UserId{ get; set; }

    [FromClaim(ClaimTypes.Role)] 
    public string[] Roles { get; set; } = [];
    public string? Sorts { get; set; } // Optional search term
    public string? Filters { get; set; } // Optional search term
    public int Skip { get; set; } = 0; // Pagination: Number of records to skip
    public int Take { get; set; } = 10; // Pagination: Number of records to take
}