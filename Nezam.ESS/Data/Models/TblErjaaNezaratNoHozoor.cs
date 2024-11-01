namespace Nezam.ESS.backend.Data.Models;

public class TblErjaaNezaratNoHozoor
{
    public long Id { get; set; }

    public long? OzviyatNo { get; set; }

    public string? StartDat { get; set; }

    public string? EndDat { get; set; }

    public int? State { get; set; }

    public string? Comments { get; set; }

    public DateTime? SabtDat { get; set; }

    public int? KarbarId { get; set; }

    public virtual TblEngineer? OzviyatNoNavigation { get; set; }
}