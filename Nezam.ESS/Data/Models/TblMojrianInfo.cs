namespace Nezam.ESS.backend.Data.Models;

public class TblMojrianInfo
{
    public long MojriId { get; set; }

    public string? MName { get; set; }

    public int? MojriType { get; set; }

    public int? FaaliatType { get; set; }

    public int? DNemayandegiCod { get; set; }

    public string? SabtNo { get; set; }

    public string? TasisDat { get; set; }

    public string? DaftarKhanehNo { get; set; }

    public string? DaftarKhanehLocation { get; set; }

    public string? MOzviyatNo { get; set; }

    public string? MojriParvanehNo { get; set; }

    public string? Address { get; set; }

    public string? CodPosti { get; set; }

    public int? ActiveType { get; set; }

    public int? ParvanehSodoorCity { get; set; }

    public int? HozeFaaliyatCod { get; set; }

    public string? ParvanehSodoorDat { get; set; }

    public string? ParvanehTamdidDat { get; set; }

    public string? ParvanehExpDat { get; set; }

    public DateTime? SabtDate { get; set; }

    public int? KarbarId { get; set; }

    public string? UserName { get; set; }

    public string? Password { get; set; }

    public string? Comments { get; set; }

    public string? MMobNo { get; set; }

    public int? PayehCod { get; set; }

    public double? Zarib { get; set; }

    public string? ExtraComments { get; set; }

    public long? ShenMelli { get; set; }

    public virtual ICollection<TblMojrianMember> TblMojrianMembers { get; set; } = new List<TblMojrianMember>();

    public virtual ICollection<TblMojrianSahmiehSaliane> TblMojrianSahmiehSalianes { get; set; } =
        new List<TblMojrianSahmiehSaliane>();

    public virtual ICollection<TblMojrianSahmieh> TblMojrianSahmiehs { get; set; } = new List<TblMojrianSahmieh>();
}