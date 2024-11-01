namespace Nezam.ESS.backend.Data.Models;

public class TblMojrianMember
{
    public long Id { get; set; }

    public long? MojriId { get; set; }

    public long? OzviyatNo { get; set; }

    public bool? Active { get; set; }

    public int? OzvType { get; set; }

    public DateTime? ActiveDat { get; set; }

    public int? KarbarId { get; set; }

    public string? Comments { get; set; }

    public virtual TblMojrianInfo? Mojri { get; set; }

    public virtual TblEngineer? OzviyatNoNavigation { get; set; }
}