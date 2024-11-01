namespace Nezam.ESS.backend.Data.Models;

public class TblMaliyatPardakhti
{
    public long Id { get; set; }

    public long? OzviyatNo { get; set; }

    public long? SabtNo { get; set; }

    public long? Mablag { get; set; }

    public DateTime? EnSabtDat { get; set; }

    public int? Sal { get; set; }

    public int? RdMaliyat { get; set; }

    public long? MaliyatMablag { get; set; }

    public virtual TblEngineer? OzviyatNoNavigation { get; set; }
}