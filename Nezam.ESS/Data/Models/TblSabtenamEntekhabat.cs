namespace Nezam.ESS.backend.Data.Models;

public class TblSabtenamEntekhabat
{
    public long Id { get; set; }

    public long OzviyatNo { get; set; }

    public string? DatTime { get; set; }

    public string? Dat { get; set; }

    public string? Comments { get; set; }

    public string? SarresidDat { get; set; }

    public string? SarresdComment { get; set; }

    public int Sal { get; set; }

    public int? KarbarId { get; set; }

    public virtual TblEngineer OzviyatNoNavigation { get; set; } = null!;
}