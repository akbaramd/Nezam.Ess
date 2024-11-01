namespace Nezam.ESS.backend.Data.Models;

public class TblErjaaNezaratRejectPenalty
{
    public long Id { get; set; }

    public int? MapSpoolId { get; set; }

    public long? OzviyatNo { get; set; }

    public int? Sal { get; set; }

    public double? PenaltyPercect { get; set; }

    public DateTime? Dat { get; set; }

    public int? KarbarId { get; set; }

    public string? Comments { get; set; }

    public virtual TblEngineer? OzviyatNoNavigation { get; set; }
}