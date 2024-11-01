namespace Nezam.ESS.backend.Data.Models;

public class TblNcontBazdid
{
    public long Id { get; set; }

    public int? MapId { get; set; }

    public int? Sal { get; set; }

    public int? Mah { get; set; }

    public int? Rooz { get; set; }

    public string? Dat { get; set; }

    public int? CodForm { get; set; }

    public string? FormName { get; set; }

    public int? SagfNo { get; set; }

    public long? OzviyatNo { get; set; }

    public int? CodReshteh { get; set; }

    public string? Comments { get; set; }

    public bool? BazdidCost { get; set; }

    public int? RdGeymati { get; set; }

    public DateTime? SabtDat { get; set; }

    public int? DNemayandegiCod { get; set; }

    public virtual TblEngineer? OzviyatNoNavigation { get; set; }
}