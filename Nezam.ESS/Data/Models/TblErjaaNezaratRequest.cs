namespace Nezam.ESS.backend.Data.Models;

public class TblErjaaNezaratRequest
{
    public long Id { get; set; }

    public long OzviyatNo { get; set; }

    public bool StatusCod { get; set; }

    public DateTime SabtDat { get; set; }

    public int? Sal { get; set; }

    public int? SabtKarbarId { get; set; }

    public DateTime? SiteTayidDat { get; set; }

    public int? ForcibleErjaa { get; set; }

    public bool? EghdamMelliMaskan { get; set; }

    public virtual TblEngineer OzviyatNoNavigation { get; set; } = null!;
}