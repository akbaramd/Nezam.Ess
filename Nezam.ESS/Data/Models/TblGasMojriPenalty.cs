namespace Nezam.ESS.backend.Data.Models;

public class TblGasMojriPenalty
{
    public long Id { get; set; }

    public int? MojriCod { get; set; }

    public int? Sal { get; set; }

    public int? Mah { get; set; }

    public DateTime? EnDat { get; set; }

    public string? Dat { get; set; }

    public int? Weight { get; set; }

    public string? Comment { get; set; }

    public int? PenaltyType { get; set; }
}