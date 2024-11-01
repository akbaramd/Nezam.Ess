namespace Nezam.ESS.backend.Data.Models;

public class TblGeoBetonDtl
{
    public int BetonIdDtl { get; set; }

    public int BetonId { get; set; }

    public string? InstanceId { get; set; }

    public int? InstanceAge { get; set; }

    public double? CubicStrength { get; set; }

    public double? CylandricStrength { get; set; }
}