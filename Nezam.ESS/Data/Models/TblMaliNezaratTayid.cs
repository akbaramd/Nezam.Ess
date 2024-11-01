namespace Nezam.ESS.backend.Data.Models;

public class TblMaliNezaratTayid
{
    public long Id { get; set; }

    public int MapId { get; set; }

    public int Sal { get; set; }

    public int CodReshteh { get; set; }

    public int CodForm { get; set; }

    public string? Dat { get; set; }

    public DateTime? EnDat { get; set; }

    public int? KarbarId { get; set; }

    public bool StatusCod { get; set; }

    public bool? Asli { get; set; }
}