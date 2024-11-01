namespace Nezam.ESS.backend.Data.Models;

public class TblDafaterFanniInfo
{
    public int DaftarNo { get; set; }

    public int? DaftarType { get; set; }

    public int? CityCod { get; set; }

    public string? Address { get; set; }

    public int? StatusCod { get; set; }

    public string? MosharekatNameNo { get; set; }

    public string? MosharekatNameDat { get; set; }

    public string? DaftarkhanehNo { get; set; }

    public int? DaftarkhanehCity { get; set; }

    public string? MojavvezNo { get; set; }

    public int? MojavvezSodoorCity { get; set; }

    public int? MojavvezOstan { get; set; }

    public string? SodoorDat { get; set; }

    public string? TamdidDat { get; set; }

    public string? ExireDat { get; set; }

    public double? Zarib { get; set; }

    public string? Comments { get; set; }

    public string? DaftarName { get; set; }

    public string? DaftarMobNo { get; set; }

    public string? DaftarTelNo { get; set; }

    public string? DaftarEMail { get; set; }

    public string? Username { get; set; }

    public string? Password { get; set; }

    public long? ShenMelli { get; set; }

    public virtual ICollection<TblDafaterFanniZarib> TblDafaterFanniZaribs { get; set; } =
        new List<TblDafaterFanniZarib>();
}