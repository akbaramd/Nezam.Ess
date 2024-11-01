namespace Nezam.ESS.backend.Data.Models;

public class TblCommunityAdvocate
{
    public long Id { get; set; }

    public int TypeId { get; set; }

    public long OzvVakil { get; set; }

    public long OzvVekalat { get; set; }

    public string? VekalatName { get; set; }

    public DateTime? SabtDat { get; set; }

    public int? KarbarId { get; set; }

    public virtual TblEngineer OzvVakilNavigation { get; set; } = null!;
}