namespace Nezam.ESS.backend.Data.Models;

public class TblMainParvanehFish
{
    public long Id { get; set; }

    public long? OzviyatNo { get; set; }

    public string? FishNo { get; set; }

    public string? VarizDat { get; set; }

    public string? SeriFish { get; set; }

    public long? Mablag { get; set; }

    public int? KarbarId { get; set; }

    public DateTime? SabtDat { get; set; }

    public int? MemberType { get; set; }

    public string? Comments { get; set; }

    public virtual TblEngineer? OzviyatNoNavigation { get; set; }
}