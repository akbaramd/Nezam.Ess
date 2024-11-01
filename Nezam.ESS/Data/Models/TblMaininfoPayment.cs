namespace Nezam.ESS.backend.Data.Models;

public class TblMaininfoPayment
{
    public long Id { get; set; }

    public int? DNemayandegiCod { get; set; }

    public string? MerchantId { get; set; }

    public int? Mapid { get; set; }

    public int? Sal { get; set; }

    public long? Mablag { get; set; }

    public string? FishNo { get; set; }

    public string? SeriFish { get; set; }

    public DateTime? VarizDate { get; set; }

    public int? KarbarId { get; set; }

    public DateTime? SabtDate { get; set; }

    public string? Comments { get; set; }

    public int? State { get; set; }
}