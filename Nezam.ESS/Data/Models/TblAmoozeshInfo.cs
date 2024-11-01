namespace Nezam.ESS.backend.Data.Models;

public class TblAmoozeshInfo
{
    public long Id { get; set; }

    public long OzviyatNo { get; set; }

    public int DoreCod { get; set; }

    public string? SabtDat { get; set; }

    public DateTime? EnDat { get; set; }

    public int? Tayid { get; set; }

    public string? FishNo { get; set; }

    public string? FishDat { get; set; }

    public long? MablagVarizi { get; set; }

    public string? Comments { get; set; }

    public DateTime? VarizDat { get; set; }

    public virtual TblEngineer OzviyatNoNavigation { get; set; } = null!;
}