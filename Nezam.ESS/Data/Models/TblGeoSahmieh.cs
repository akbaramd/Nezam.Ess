namespace Nezam.ESS.backend.Data.Models;

public class TblGeoSahmieh
{
    public int MapGeoId { get; set; }

    public long LegalMemberId { get; set; }

    public int GeoTypeId { get; set; }

    public double? Tedad { get; set; }

    public double? Metraj { get; set; }

    public DateTime? SabtDate { get; set; }

    public int? KarbarId { get; set; }

    public bool? Isfree { get; set; }

    public DateTime? DFree { get; set; }

    public int? KarbarIdFree { get; set; }

    public int Id { get; set; }

    public long? InspectorId { get; set; }

    public DateTime? OldDat { get; set; }

    public string? Comments { get; set; }

    public DateTime? CedoRegDate { get; set; }

    public DateTime? InspectorSabtDat { get; set; }

    public long? InvolvedMemberId { get; set; }

    public virtual TblEngineer? Inspector { get; set; }

    public virtual TblGeoMaininfo MapGeo { get; set; } = null!;
}