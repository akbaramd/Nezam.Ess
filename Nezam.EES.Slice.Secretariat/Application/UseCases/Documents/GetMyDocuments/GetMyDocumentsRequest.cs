namespace Nezam.EES.Slice.Secretariat.Application.UseCases.Documents.GetMyDocuments;

public class GetMyDocumentsRequest
{
    public string? Search { get; set; } // Optional search term
    public int Skip { get; set; } = 0; // Pagination: Number of records to skip
    public int Take { get; set; } = 10;
}
