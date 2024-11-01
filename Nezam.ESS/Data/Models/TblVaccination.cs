namespace Nezam.ESS.backend.Data.Models;

public class TblVaccination
{
    public long Id { get; set; }

    public long? OzviyatNo { get; set; }

    public DateTime? Dat { get; set; }

    public string? SpouseName { get; set; }

    public string? MelliCod { get; set; }

    public string? SpouseBirthDate { get; set; }

    public string? Bimari { get; set; }

    public int? State { get; set; }

    public int? Ersal { get; set; }

    public virtual TblEngineer? OzviyatNoNavigation { get; set; }
}