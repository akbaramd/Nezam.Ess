namespace Nezam.ESS.backend.Data.Models;

public class TblMaliHasabNo
{
    public int BankCod { get; set; }

    public long OzviyatNo { get; set; }

    public string? HesabNo { get; set; }

    public long Id { get; set; }

    public int? KarbarId { get; set; }

    public DateTime? SabtDat { get; set; }

    public virtual TblEngineer OzviyatNoNavigation { get; set; } = null!;
}