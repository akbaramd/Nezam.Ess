namespace Nezam.ESS.backend.Data.Models;

public class TblErjaaNezaratEngBan
{
    public long Id { get; set; }

    public long OzviyatNo { get; set; }

    public DateTime? SabtDat { get; set; }

    public int Sal { get; set; }

    public string Comment { get; set; } = null!;

    public bool? State { get; set; }

    public virtual TblEngineer OzviyatNoNavigation { get; set; } = null!;
}