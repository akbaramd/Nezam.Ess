namespace Nezam.ESS.backend.Data.Models;

public class TblGeoMaininfo
{
    public int MapGeoId { get; set; }

    public int DNemayandegiCod { get; set; }

    public int? Id { get; set; }

    public int? Sal { get; set; }

    public string? Name { get; set; }

    public string? Fname { get; set; }

    public string? DastoorNo { get; set; }

    public string? DastoorDate { get; set; }

    public string? PelakNo { get; set; }

    public double? Metraj { get; set; }

    public int? TabageNo { get; set; }

    public int? TedadSagf { get; set; }

    public int? Gorooh { get; set; }

    public int? Mantageh { get; set; }

    public string? ParvaneNo { get; set; }

    public string? ParvanehDate { get; set; }

    public int? ParvanehMetraj { get; set; }

    public string? Comment { get; set; }

    public int? ChangeType { get; set; }

    public int? Mah { get; set; }

    public int? Rooz { get; set; }

    public string? SodoorDate { get; set; }

    public string? MalekAddress { get; set; }

    public string? MalekMobNo { get; set; }

    public int? SkeletType { get; set; }

    public int? KarbariType { get; set; }

    public string? MalekMelliCod { get; set; }

    public string? MalekAddressEgamat { get; set; }

    public DateTime? Dsabt { get; set; }

    public virtual ICollection<TblGeoSahmieh> TblGeoSahmiehs { get; set; } = new List<TblGeoSahmieh>();
}