namespace Nezam.EES.Slice.Secretariat.Application.UseCases.Participants.GetParticipants;

public class GetParticipantsRequest
{
    public string? Search { get; set; } // Optional search term
    public int Skip { get; set; } = 0; // Pagination: Number of records to skip
    public int Take { get; set; } = 10; // Pagination: Number of records to take
}