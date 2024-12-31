using FastEndpoints;

namespace Nezam.EES.Slice.Secretariat.Application.UseCases.Documents.GetMyDocuments;

public class GetMyDocumentsRequest
{
    // Pagination
    public int Skip { get; set; } = 0; // Number of records to skip
    public int Take { get; set; } = 10; // Number of records to take

    // Filtering
    [QueryParam]
    public string? TrackingCode { get; set; }
    [QueryParam]
    public int? LetterNumber { get; set; }
    [QueryParam]
    public DateTime? LetterDate { get; set; }
    [QueryParam]
    public string? Title { get; set; }

    // Sorting
    [QueryParam]
    public List<SortOption>? SortOptions { get; set; }
}

public class SortOption
{
    public string Field { get; set; } = string.Empty; // e.g., "Title", "LetterDate"
    public bool IsDescending { get; set; } = false;
}