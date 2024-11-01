namespace Nezam.ESS.backend.Data.Models;

public class TblTafkikEngineer
{
    public long EngCod { get; set; }

    public long OzviyatNo { get; set; }

    public int Sal { get; set; }

    public bool? Active { get; set; }

    public string? Comments { get; set; }

    public virtual TblEngineer OzviyatNoNavigation { get; set; } = null!;
}