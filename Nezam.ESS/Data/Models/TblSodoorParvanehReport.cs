namespace Nezam.ESS.backend.Data.Models;

public class TblSodoorParvanehReport
{
    public long Id { get; set; }

    public long? ParvanehNo { get; set; }

    public string? ParvanehSerialNo { get; set; }

    public string? Dat { get; set; }

    public string? Saat { get; set; }

    public string? Comments { get; set; }

    public string? KarbarId { get; set; }

    public int? SodoorType { get; set; }
}