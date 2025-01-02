using FastEndpoints;

namespace Nezam.EES.Slice.Secretariat.Application.UseCases.Documents.GetMyDocuments;

public class GetMyDocumentsRequest
{
    // Pagination
    public int Skip { get; set; } = 0; // Number of records to skip
    public int Take { get; set; } = 10; // Number of records to take

    // Filtering
    [QueryParam]
    public string? Filters { get; set; } // e.g., "TrackingCode:ABC,Title:Report"
    
    // Sorting
    [QueryParam]
    public string? Sorting { get; set; } // e.g., "Title:asc,LetterDate:desc"
}