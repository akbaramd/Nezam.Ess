namespace Nezam.ESS.backend.Data.Models;

public class TblBargMaininfoPayment
{
    public int IdDtl { get; set; }

    public int? MapId { get; set; }

    public string ShomFish { get; set; } = null!;

    public string SeriFish { get; set; } = null!;

    public string? TarikhFish { get; set; }

    public long? MablagFish { get; set; }

    public int? KarbarId { get; set; }

    public bool? FromSite { get; set; }

    public DateTime? SabtDat { get; set; }

    public string? Comments { get; set; }
}