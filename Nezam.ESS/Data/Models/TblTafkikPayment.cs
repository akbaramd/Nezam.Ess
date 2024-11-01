namespace Nezam.ESS.backend.Data.Models;

public class TblTafkikPayment
{
    public long Id { get; set; }

    public int? TafkikSabtNo { get; set; }

    public int? Sal { get; set; }

    public long? Mablag { get; set; }

    public string? FishNo { get; set; }

    public string? SeriFish { get; set; }

    public string? VarizDat { get; set; }

    public DateTime? SabtDat { get; set; }

    public int? KarbarId { get; set; }

    public string? Comments { get; set; }

    public int? State { get; set; }
}