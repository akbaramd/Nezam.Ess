namespace Nezam.ESS.backend.Data.Models;

public class TblMadde27Sahmieh
{
    public long Id { get; set; }

    public long? ParvandehNo { get; set; }

    public int? Sal { get; set; }

    public long? OzviyatNo { get; set; }

    public DateTime? SabtDat { get; set; }

    public int? KarbarId { get; set; }

    public double? MablagKarshenasi { get; set; }

    public virtual TblEngineer? OzviyatNoNavigation { get; set; }
}