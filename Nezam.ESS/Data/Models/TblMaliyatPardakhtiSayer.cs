namespace Nezam.ESS.backend.Data.Models;

public class TblMaliyatPardakhtiSayer
{
    public long Id { get; set; }

    public long? OzviyatNo { get; set; }

    public long? Mablag { get; set; }

    public int? Sal { get; set; }

    public int? RdMaliyat { get; set; }

    public int? PardakhtGroup { get; set; }

    public double? MablagMaliyat { get; set; }

    public virtual TblEngineer? OzviyatNoNavigation { get; set; }
}