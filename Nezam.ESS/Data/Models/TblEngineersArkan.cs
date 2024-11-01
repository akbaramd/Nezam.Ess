namespace Nezam.ESS.backend.Data.Models;

public class TblEngineersArkan
{
    public long Id { get; set; }

    public long? OzviyatNo { get; set; }

    public int? ArkanStatusCod { get; set; }

    public int? DoreCod { get; set; }

    public string? Comments { get; set; }

    public bool? Active { get; set; }

    public bool? Member { get; set; }

    public string? Name { get; set; }

    public string? Fname { get; set; }

    public string? MobileNo { get; set; }

    public virtual TblEngineer? OzviyatNoNavigation { get; set; }
}