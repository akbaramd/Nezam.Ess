namespace Nezam.ESS.backend.Data.Models;

public class TblErjaaNezaratSahmiehChoice
{
    public long Id { get; set; }

    public int? MapId { get; set; }

    public int? Sal { get; set; }

    public long? OzviyatNo { get; set; }

    public DateTime? SabtDat { get; set; }

    public bool? State { get; set; }

    public DateTime? TayidDat { get; set; }

    public int? TayidKarbarId { get; set; }

    public virtual TblEngineer? OzviyatNoNavigation { get; set; }
}