namespace Nezam.ESS.backend.Data.Models;

public class TblMaliHasabNoLog
{
    public long Id { get; set; }

    public long? OzviyatNo { get; set; }

    public int? KarbarId { get; set; }

    public DateTime? DelDat { get; set; }

    public string? OldHesabNo { get; set; }

    public virtual TblEngineer? OzviyatNoNavigation { get; set; }
}