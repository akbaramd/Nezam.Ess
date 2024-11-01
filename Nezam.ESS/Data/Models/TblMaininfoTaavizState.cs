namespace Nezam.ESS.backend.Data.Models;

public class TblMaininfoTaavizState
{
    public long Id { get; set; }

    public int? MapId { get; set; }

    public int? Sal { get; set; }

    public int? DNemayandegiCod { get; set; }

    public string? Mapstate { get; set; }

    public bool? ActiveType { get; set; }

    public int? KarbarId { get; set; }

    public DateTime? SabtDat { get; set; }

    public int? DelKarbarId { get; set; }

    public DateTime? DelDat { get; set; }
}