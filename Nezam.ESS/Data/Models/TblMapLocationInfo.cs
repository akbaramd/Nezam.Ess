namespace Nezam.ESS.backend.Data.Models;

public class TblMapLocationInfo
{
    public long Id { get; set; }

    public int? MapId { get; set; }

    public int? Sal { get; set; }

    public int? StatusCod { get; set; }

    public DateTime? Dat { get; set; }

    public int? KarbarId { get; set; }

    public string? Comments { get; set; }

    public int? VahedCod { get; set; }

    public string? ContOzvNo { get; set; }
}