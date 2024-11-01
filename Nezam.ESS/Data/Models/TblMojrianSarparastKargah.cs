namespace Nezam.ESS.backend.Data.Models;

public class TblMojrianSarparastKargah
{
    public long Id { get; set; }

    public int? MapId { get; set; }

    public int? Sal { get; set; }

    public DateTime? SabtDat { get; set; }

    public long? OzviyatNo { get; set; }

    public bool? Free { get; set; }

    public string? Comments { get; set; }

    public int? KarbarId { get; set; }

    public bool? Active { get; set; }

    public string? UnactiveDat { get; set; }

    public int? UnactiveKarbarId { get; set; }

    public int? FreeKarbarId { get; set; }

    public string? FreeDat { get; set; }

    public int? DNemayandegiCod { get; set; }

    public virtual TblEngineer? OzviyatNoNavigation { get; set; }
}