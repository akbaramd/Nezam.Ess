namespace Nezam.ESS.backend.Data.Models;

public class TblPishnahadat
{
    public long Id { get; set; }

    public long? OzviyatNo { get; set; }

    public int? ErjaaTo { get; set; }

    public DateTime? DatTime { get; set; }

    public string? Dat { get; set; }

    public string? Comment { get; set; }

    public string? Title { get; set; }

    public int? Status { get; set; }

    public virtual TblEngineer? OzviyatNoNavigation { get; set; }
}