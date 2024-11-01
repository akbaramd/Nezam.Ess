namespace Nezam.ESS.backend.Data.Models;

public class TblSahmiehModification
{
    public long Id { get; set; }

    public int? CitySyncCode { get; set; }

    public long? DossierNumber { get; set; }

    public int? MemberType { get; set; }

    public int? ServiceType { get; set; }

    public int? SyncCode { get; set; }

    public int? ServiceField { get; set; }

    public double? WorkItem { get; set; }

    public DateTime? Dat { get; set; }

    public int? KarbarId { get; set; }

    public string? Comments { get; set; }
}