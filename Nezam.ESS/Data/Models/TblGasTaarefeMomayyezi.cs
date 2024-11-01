namespace Nezam.ESS.backend.Data.Models;

public class TblGasTaarefeMomayyezi
{
    public int Id { get; set; }

    public long? Mablag { get; set; }

    public int Sal { get; set; }

    public string? Comments { get; set; }

    public int MomayyeziTypeCod { get; set; }

    public int RdGeymati { get; set; }

    public int? VahedBazdidRate { get; set; }

    public double? SahmSazman { get; set; }

    public double? ArzeshAfz { get; set; }
}