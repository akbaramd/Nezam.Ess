namespace Nezam.ESS.backend.Data.Models;

public class TblSodoorFormList
{
    public long Id { get; set; }

    public long? OzviyatNo { get; set; }

    public int? KarbarId { get; set; }

    public DateTime? Dat { get; set; }

    public int? FormCod { get; set; }

    public virtual TblEngineer? OzviyatNoNavigation { get; set; }
}