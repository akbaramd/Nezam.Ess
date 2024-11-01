namespace Nezam.ESS.backend.Data.Models;

public class TblSahmiyeFullTime
{
    public long Id { get; set; }

    public long? OzviyatNo { get; set; }

    public int? Sal { get; set; }

    public string? Comment { get; set; }

    public int? State { get; set; }

    public DateTime? SabtDat { get; set; }

    public double? Perc { get; set; }

    public int? KarbarId { get; set; }

    public int? DelKarbarId { get; set; }

    public DateTime? DelDat { get; set; }

    public virtual TblEngineer? OzviyatNoNavigation { get; set; }
}