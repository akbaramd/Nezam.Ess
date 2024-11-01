namespace Nezam.ESS.backend.Data.Models;

public class TblCompetence
{
    public long Rd { get; set; }

    public int? FlCode { get; set; }

    public int? PayehNezarat { get; set; }

    public int? PayehTarrahi { get; set; }

    public int? PayehMohasebat { get; set; }

    public string? CmpDat { get; set; }

    public int? MaghtaCod { get; set; }

    public int? Exam { get; set; }

    public long? ParvanehNo { get; set; }

    public int? Justice { get; set; }

    public int? Payeh { get; set; }

    public int? PnazaratCod { get; set; }

    public int? PtarrahiCod { get; set; }

    public int? PayehType { get; set; }

    public byte[]? SignatureSample { get; set; }

    public string? KarbarId { get; set; }

    public long? OzviyatNo { get; set; }

    public virtual TblEngineer? OzviyatNoNavigation { get; set; }
}