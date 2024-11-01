namespace Nezam.ESS.backend.Data.Models;

public class TblNcontEng
{
    public int Id { get; set; }

    public long? OzviyatNo { get; set; }

    public int? Term { get; set; }

    public bool? Active { get; set; }

    public string? Comments { get; set; }

    public DateTime? SabtDate { get; set; }

    public int? DNemayandegiCod { get; set; }

    public virtual TblEngineer? OzviyatNoNavigation { get; set; }
}