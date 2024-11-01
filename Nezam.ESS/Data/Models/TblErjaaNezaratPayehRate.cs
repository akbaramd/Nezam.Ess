namespace Nezam.ESS.backend.Data.Models;

public class TblErjaaNezaratPayehRate
{
    public int Id { get; set; }

    public int? PayehNezarat { get; set; }

    public int? GoroohCod { get; set; }

    public int? Sal { get; set; }

    public double? Rate { get; set; }

    public string? Comment { get; set; }
}