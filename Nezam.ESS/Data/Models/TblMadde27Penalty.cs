namespace Nezam.ESS.backend.Data.Models;

public class TblMadde27Penalty
{
    public long Id { get; set; }

    public long? OzviyatNo { get; set; }

    public int? Sal { get; set; }

    public int? TypeCod { get; set; }

    public DateTime? Dat { get; set; }

    public int? KarbarId { get; set; }

    public double? Mablag { get; set; }

    public string? Description { get; set; }

    public int? ParvandehType { get; set; }

    public virtual TblEngineer? OzviyatNoNavigation { get; set; }
}