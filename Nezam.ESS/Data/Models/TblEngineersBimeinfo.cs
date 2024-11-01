namespace Nezam.ESS.backend.Data.Models;

public class TblEngineersBimeinfo
{
    public long Id { get; set; }

    public long OzviyatNo { get; set; }

    public string? BimeNo { get; set; }

    public string? StartDate { get; set; }

    public bool? Active { get; set; }

    public string? Comments { get; set; }

    public virtual TblEngineer OzviyatNoNavigation { get; set; } = null!;
}