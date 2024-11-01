namespace Nezam.ESS.backend.Data.Models;

public class TblRestriction
{
    public long Id { get; set; }

    public string? SabtDate { get; set; }

    public long? OzviyatNo { get; set; }

    public string? Name { get; set; }

    public string? Fname { get; set; }

    public int? RestrictCod { get; set; }

    public string? ShakiName { get; set; }

    public int? Sal { get; set; }

    public string? StartDate { get; set; }

    public string? EndDate { get; set; }

    public string? Comments { get; set; }

    public string? SabtTime { get; set; }

    public DateTime? EnSabt { get; set; }

    public virtual TblEngineer? OzviyatNoNavigation { get; set; }
}