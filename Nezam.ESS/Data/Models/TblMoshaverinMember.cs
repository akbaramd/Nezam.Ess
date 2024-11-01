namespace Nezam.ESS.backend.Data.Models;

public class TblMoshaverinMember
{
    public long Id { get; set; }

    public long? MoshaverId { get; set; }

    public long? OzviyatNo { get; set; }

    public bool? Active { get; set; }

    public int? OzvType { get; set; }

    public DateTime? ActiveDat { get; set; }

    public int? KarbarId { get; set; }

    public string? Comments { get; set; }

    public virtual TblMoshaverinInfo? Moshaver { get; set; }

    public virtual TblEngineer? OzviyatNoNavigation { get; set; }
}