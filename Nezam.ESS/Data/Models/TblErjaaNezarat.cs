namespace Nezam.ESS.backend.Data.Models;

public class TblErjaaNezarat
{
    public long Id { get; set; }

    public long OzviyatNo { get; set; }

    public int Sal { get; set; }

    public int? TedadErjaa { get; set; }

    public long? SumMetraj { get; set; }

    public string? LastSabtDate { get; set; }

    public string? Comments { get; set; }

    public bool? OmranGAlefOk { get; set; }

    public bool? Active { get; set; }

    public bool? OmranGBOk { get; set; }

    public virtual TblEngineer OzviyatNoNavigation { get; set; } = null!;
}