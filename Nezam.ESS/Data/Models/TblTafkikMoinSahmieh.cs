namespace Nezam.ESS.backend.Data.Models;

public class TblTafkikMoinSahmieh
{
    public long MoinCod { get; set; }

    public int MapId { get; set; }

    public int Sal { get; set; }

    public string? SabtDat { get; set; }

    public bool? BazdidCost { get; set; }

    public DateTime EnDat { get; set; }

    public string? Comments { get; set; }
}