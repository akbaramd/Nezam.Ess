namespace Nezam.ESS.backend.Data.Models;

public class TblUtmPayment
{
    public long Id { get; set; }

    public long? Utminfoid { get; set; }

    public string ShomFish { get; set; } = null!;

    public string SeriFish { get; set; } = null!;

    public string? TarikhFish { get; set; }

    public long? MablagFish { get; set; }

    public int? KarbarId { get; set; }

    public bool? FromSite { get; set; }

    public DateTime? SabtDat { get; set; }

    public string? Comments { get; set; }
}