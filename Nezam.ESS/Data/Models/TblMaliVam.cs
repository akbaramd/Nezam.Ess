namespace Nezam.ESS.backend.Data.Models;

public class TblMaliVam
{
    public long Id { get; set; }

    public long? OzviyatNo { get; set; }

    public int? VamType { get; set; }

    public DateTime? SabtDate { get; set; }

    public string? Comments { get; set; }

    public double? Mablag { get; set; }

    public string? DarkhastNo { get; set; }

    public string? DarkhastDat { get; set; }

    public int? KarbarId { get; set; }

    public int? State { get; set; }

    public int? DelKarbarId { get; set; }

    public DateTime? DelDate { get; set; }

    public string? DelReason { get; set; }

    public virtual TblEngineer? OzviyatNoNavigation { get; set; }
}