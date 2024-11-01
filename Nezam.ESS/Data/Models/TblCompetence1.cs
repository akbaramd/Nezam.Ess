namespace Nezam.ESS.backend.Data.Models;

public class TblCompetence1
{
    public long Id { get; set; }

    public long? OzviyatNo { get; set; }

    public string? CompEhrazDat { get; set; }

    public string? CompAkhzDat { get; set; }

    public int? CompCod { get; set; }

    public DateTime? SabtDat { get; set; }

    public int? KarbarId { get; set; }

    public string? Comments { get; set; }

    public int? MaghtaCod { get; set; }

    public int? FlCod { get; set; }

    public virtual TblEngineer? OzviyatNoNavigation { get; set; }
}