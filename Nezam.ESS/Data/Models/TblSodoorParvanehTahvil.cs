namespace Nezam.ESS.backend.Data.Models;

public class TblSodoorParvanehTahvil
{
    public long Id { get; set; }

    public long? OzviyatNo { get; set; }

    public DateTime? Dat { get; set; }

    public int? State { get; set; }

    public int? KarbarId { get; set; }

    public string? Comments { get; set; }

    public virtual TblEngineer? OzviyatNoNavigation { get; set; }
}