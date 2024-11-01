namespace Nezam.ESS.backend.Data.Models;

public class TblDafaterFanniZarib
{
    public long Id { get; set; }

    public int DaftarNo { get; set; }

    public double? Zarib { get; set; }

    public int CodReshteh { get; set; }

    public string? Comments { get; set; }

    public DateTime? SabtDate { get; set; }

    public int? KarbarId { get; set; }

    public virtual TblDafaterFanniInfo DaftarNoNavigation { get; set; } = null!;
}