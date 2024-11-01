namespace Nezam.ESS.backend.Data.Models;

public class TblUtmEng
{
    public int Id { get; set; }

    public int? EngCod { get; set; }

    public long? OzviyatNo { get; set; }

    public int? ActiveType { get; set; }

    public DateTime? SabtDat { get; set; }

    public int? KarbarId { get; set; }

    public string? Comments { get; set; }

    public virtual TblEngineer? OzviyatNoNavigation { get; set; }
    public virtual ICollection<TblUtmSahmieh>? TblUtmSahmiehs { get; set; }
}