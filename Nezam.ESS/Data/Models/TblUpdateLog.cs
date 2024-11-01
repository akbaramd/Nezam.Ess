namespace Nezam.ESS.backend.Data.Models;

public class TblUpdateLog
{
    public int Id { get; set; }

    public DateTime? Dat { get; set; }

    public int? KarbarId { get; set; }

    public string? Filename { get; set; }
}