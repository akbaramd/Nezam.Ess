namespace Nezam.ESS.backend.Data.Models;

public class TblBargBazrasin
{
    public int BazrasCod { get; set; }

    public long OzviyatNo { get; set; }

    public byte? Status { get; set; }

    public string? Comment { get; set; }

    public int? Payeh { get; set; }

    public int? DNemayandegiCod { get; set; }

    public virtual TblEngineer OzviyatNoNavigation { get; set; } = null!;
}