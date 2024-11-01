namespace Nezam.ESS.backend.Data.Models;

public class TblMojrianMapInfo
{
    public long Id { get; set; }

    public int? MapId { get; set; }

    public int? Sal { get; set; }

    public DateTime? SabtDat { get; set; }

    public int? StatusCod { get; set; }

    public string? Comments { get; set; }

    public int? KarbarId { get; set; }

    public bool? Active { get; set; }
}