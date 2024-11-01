namespace Nezam.ESS.backend.Data.Models;

public class TblMapControlDastoorMember
{
    public int Id { get; set; }

    public long? OzviyatNo { get; set; }

    public DateTime? SabtDate { get; set; }

    public bool? ActiveType { get; set; }

    public bool? Head { get; set; }

    public string? Comments { get; set; }

    public virtual TblEngineer? OzviyatNoNavigation { get; set; }
}