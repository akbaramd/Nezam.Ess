namespace Nezam.ESS.backend.Data.Models;

public class TblTafkikPenalty
{
    public int Id { get; set; }

    public int? EngCod { get; set; }

    public int? Penalty { get; set; }

    public string? Dsc { get; set; }

    public int? State { get; set; }

    public DateTime? Dsabt { get; set; }

    public int? Sal { get; set; }
}