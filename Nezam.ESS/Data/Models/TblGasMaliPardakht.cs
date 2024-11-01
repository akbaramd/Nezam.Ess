namespace Nezam.ESS.backend.Data.Models;

public class TblGasMaliPardakht
{
    public long Id { get; set; }

    public int? EngCod { get; set; }

    public long? Mablag { get; set; }

    public string? DatPardakht { get; set; }

    public DateTime? SabtDat { get; set; }

    /// <summary>
    ///     for bazdid mojaddad and pardakht normal
    /// </summary>
    public int? PardakhtType { get; set; }

    public string? Comments { get; set; }
}