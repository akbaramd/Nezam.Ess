namespace Nezam.ESS.backend.Data.Models;

public class TblGasMaliFish
{
    public string ShomFish { get; set; } = null!;

    public string SeriFish { get; set; } = null!;

    public string? TarikhFish { get; set; }

    public long? MablagFish { get; set; }

    public long? IdTemp { get; set; }

    public int? KarbarId { get; set; }

    public DateTime? DTayid { get; set; }

    public bool? FromSite { get; set; }
}