namespace Nezam.ESS.backend.Data.Models;

public class TblMadde27EngsLog
{
    public int Id { get; set; }

    public long? OzviyatNo { get; set; }

    public int? KarbarId { get; set; }

    public DateTime? SabtDat { get; set; }

    public int? State { get; set; }

    public string? Comments { get; set; }

    public string? ParvanehExpDat { get; set; }

    public virtual TblEngineer? OzviyatNoNavigation { get; set; }
}