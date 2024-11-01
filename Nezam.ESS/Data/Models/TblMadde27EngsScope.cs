namespace Nezam.ESS.backend.Data.Models;

public class TblMadde27EngsScope
{
    public long OzviyatNo { get; set; }

    public int DNemayandegiCod { get; set; }

    public string? Comments { get; set; }

    public virtual TblEngineer OzviyatNoNavigation { get; set; } = null!;
}