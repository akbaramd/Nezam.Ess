namespace Nezam.ESS.backend.Data.Models;

public class TblSodoorCartReport
{
    public long Id { get; set; }

    public long? OzviyatNo { get; set; }

    public string? Dat { get; set; }

    public string? Saat { get; set; }

    public string? Comments { get; set; }

    public string? KarbarId { get; set; }

    public virtual TblEngineer? OzviyatNoNavigation { get; set; }
}