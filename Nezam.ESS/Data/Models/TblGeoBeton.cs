namespace Nezam.ESS.backend.Data.Models;

public class TblGeoBeton
{
    public int BetonId { get; set; }

    public int? MapId { get; set; }

    public int? Sal { get; set; }

    public int? LabId { get; set; }

    public int? InstanceLocationId { get; set; }

    public int? CementTypeId { get; set; }

    public string? CementGrade { get; set; }

    public int? CementProducerId { get; set; }

    public DateTime? InstanceDate { get; set; }

    public int? KarbarId { get; set; }
}