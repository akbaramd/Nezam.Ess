using FastEndpoints;

namespace Nezam.EES.Slice.Secretariat.Application.UseCases.Documents.GetMyDocuments;

public class GetMyDocumentsRequest
{
    public string? Search { get; set; } // Optional search term
    public int Skip { get; set; } = 0; // Pagination: Number of records to skip
    public int Take { get; set; } = 10;

    [QueryParam]
    public string? TrackingCode { get; set; }
    [QueryParam]
    public int? LetterNumber { get; set; }
    [QueryParam]
    public DateTime? LetterDate { get; set; }
}
