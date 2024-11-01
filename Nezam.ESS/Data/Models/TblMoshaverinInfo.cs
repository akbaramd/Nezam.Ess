namespace Nezam.ESS.backend.Data.Models;

public class TblMoshaverinInfo
{
    public long MoshaverId { get; set; }

    public string? MName { get; set; }

    public int? MoshaverType { get; set; }

    public double? Zarib { get; set; }

    public int? DNemayandegiCod { get; set; }

    public string? SabtNo { get; set; }

    public string? TasisDat { get; set; }

    public string? MOzviyatNo { get; set; }

    public string? MParvanehNo { get; set; }

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

    public string? Password { get; set; }

    public string? Comments { get; set; }

    public string? MMobNo { get; set; }

    public long? ShenMelli { get; set; }

    public virtual ICollection<TblMoshaverinMember> TblMoshaverinMembers { get; set; } =
        new List<TblMoshaverinMember>();
}