namespace Nezam.ESS.backend.Data.Models;

public class TblErjaaNezaratSahmiehTemp
{
    public long Id { get; set; }

    public long OzviyatNo { get; set; }

    public int MapId { get; set; }

    public int Sal { get; set; }

    public int? CodReshteh { get; set; }

    public double? Tedad { get; set; }

    public double? Nezarat { get; set; }

    public string? ErjaaDat { get; set; }

    public int? State { get; set; }

    public bool? KarbarTayid { get; set; }

    public DateTime? SabtDat { get; set; }

    public int? ScoreState { get; set; }

    public int? DNemayandegiCod { get; set; }

    public virtual TblEngineer OzviyatNoNavigation { get; set; } = null!;
}