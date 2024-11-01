namespace Nezam.ESS.backend.Data.Models;

public class TblMojrianSahmieh
{
    public long Id { get; set; }

    public long? MojriId { get; set; }

    public int? MapId { get; set; }

    public int? Sal { get; set; }

    public DateTime? SabtDat { get; set; }

    public double? TedadKar { get; set; }

    public bool? Free { get; set; }

    public string? FreeDat { get; set; }

    public int? FreeKarbarId { get; set; }

    public string? GarardadDat { get; set; }

    public string? GarardadExpDat { get; set; }

    public int? KarbarId { get; set; }

    public int? GarardadNo { get; set; }

    public bool? Active { get; set; }

    public string? UnactiveDat { get; set; }

    public int? UnactiveKarbarId { get; set; }

    public double? Metraj { get; set; }

    public int? FreeType { get; set; }

    public int? SahmiehType { get; set; }

    public string? Comments { get; set; }

    public int? GarardadType { get; set; }

    public string? TamdidDat { get; set; }

    public double? MablagGarardad { get; set; }

    public int? DNemayandegiCod { get; set; }

    public DateTime? CedoRegDate { get; set; }

    public long? InvolvedMemberId { get; set; }

    public virtual TblMojrianInfo? Mojri { get; set; }
}