namespace Nezam.ESS.backend.Data.Models;

public class TblShakhesPenalty
{
    public long Id { get; set; }

    public long? OzviyatNo { get; set; }

    public int? Sal { get; set; }

    public int? SabtNo { get; set; }

    public int? Weight { get; set; }

    public string? Comments { get; set; }

    public DateTime? EnDat { get; set; }

    public string? Dat { get; set; }

    public int? KarbarId { get; set; }

    public virtual TblEngineer? OzviyatNoNavigation { get; set; }
}