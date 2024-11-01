namespace Nezam.ESS.backend.Data.Models;

public class TblMadde27EngsSalahiyat
{
    public long OzviyatNo { get; set; }

    public int TypeCod { get; set; }

    public virtual TblEngineer OzviyatNoNavigation { get; set; } = null!;
}