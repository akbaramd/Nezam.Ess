namespace Nezam.ESS.backend.Data.Models;

public class TblBargPenalty
{
    public long Id { get; set; }

    public int? EngCod { get; set; }

    public int? Sal { get; set; }

    public int? TypeCod { get; set; }

    public DateTime? Dat { get; set; }

    public int? KarbarId { get; set; }

    public double? Mablag { get; set; }

    public string? Description { get; set; }

    public long? Mapid { get; set; }
}