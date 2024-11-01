namespace Nezam.ESS.backend.Data.Models;

public class TblTafkikSahmieh
{
    public long EngCod { get; set; }

    public int MapId { get; set; }

    public int Sal { get; set; }

    public string? SabtDat { get; set; }

    public bool? BazdidCost { get; set; }

    public DateTime EnDat { get; set; }

    public string? Comments { get; set; }

    public int? State { get; set; }

    public int? KarbarId { get; set; }
}