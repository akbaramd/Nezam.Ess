namespace Nezam.ESS.backend.Data.Models;

public class TblLegalMembersMember
{
    public long Id { get; set; }

    public int LegalMemberId { get; set; }

    public long? OzviyatNo { get; set; }

    public DateTime? SabtDat { get; set; }

    public int? OzvType { get; set; }

    public int? Active { get; set; }

    public string? Comments { get; set; }

    public int? KarbarId { get; set; }

    public virtual TblEngineer? OzviyatNoNavigation { get; set; }
}