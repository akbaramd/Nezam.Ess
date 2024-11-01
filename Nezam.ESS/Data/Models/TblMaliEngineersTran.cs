namespace Nezam.ESS.backend.Data.Models;

public class TblMaliEngineersTran
{
    public long Id { get; set; }

    public long OzviyatNo { get; set; }

    public DateTime? SabtDat { get; set; }

    public int? State { get; set; }

    public double? Bedehkar { get; set; }

    public double? Bestankar { get; set; }

    public DateTime? VarizDat { get; set; }

    public string? ShomFish { get; set; }

    public string? SeriFish { get; set; }

    public string? Comments { get; set; }

    public bool? FromSite { get; set; }

    public long? PaymentId { get; set; }

    public int? KarbarId { get; set; }

    public virtual TblEngineer OzviyatNoNavigation { get; set; } = null!;
}