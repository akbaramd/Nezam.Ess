namespace Nezam.EES.Slice.Secretariat.Applciation.UseCases.Documents.GetMyDocuments;

public class GetMyDocumentsRequest
{
    public int PageIndex { get; set; } = 0;
    public int PageSize { get; set; } = 10;
}
