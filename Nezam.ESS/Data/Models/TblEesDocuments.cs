namespace Nezam.ESS.backend.Data.Models;

public class TblEesDocuments
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string FilePath { get; set; }
    public string TrackingCode { get; set; }
    public long BonUserId { get; set; }
    public DateTime? CreatedAt { get; set; }
    public int State { get; set; }
    public int Type { get; set; }
}