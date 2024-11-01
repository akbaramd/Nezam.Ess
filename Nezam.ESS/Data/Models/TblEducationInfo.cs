namespace Nezam.ESS.backend.Data.Models;

public class TblEducationInfo
{
    public long Rd { get; set; }

    public int? MaghtaCod { get; set; }

    public int? FlCod { get; set; }

    public string? AkhzMadrakDat { get; set; }

    public int? AsliMadrakCod { get; set; }

    public string? CmpDat { get; set; }

    public string? AkhzMadrakRooz { get; set; }

    public string? AkhzMadrakMah { get; set; }

    public string? AkhzMadrakSal { get; set; }

    public int? ED11 { get; set; }

    public int? ED12 { get; set; }

    public int? ED13 { get; set; }

    public int? UniversityCod { get; set; }

    public int? UniversityContry { get; set; }

    public int? UniversityCity { get; set; }

    public string? Payanname { get; set; }

    public long? ParvanehNo { get; set; }

    public long? OzviyatNo { get; set; }

    public double? MoaddelKoll { get; set; }

    public virtual TblEngineer? OzviyatNoNavigation { get; set; }
}