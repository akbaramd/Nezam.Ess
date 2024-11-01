namespace Nezam.ESS.backend.Data.Models;

public class TblMaininfoMaliPardakht
{
    public long Id { get; set; }

    public string TrackCode { get; set; } = null!;

    public long? MablagFish { get; set; }

    public string? ShomFish { get; set; }

    public string? TarikhFish { get; set; }

    public int? BankId { get; set; }

    public int? KarbarId { get; set; }

    public DateTime? DPardakht { get; set; }

    public DateTime? DTayid { get; set; }
}