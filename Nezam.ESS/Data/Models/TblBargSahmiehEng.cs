namespace Nezam.ESS.backend.Data.Models;

public class TblBargSahmiehEng
{
    public int Id { get; set; }

    public long BazrasCod { get; set; }

    public int BargMapId { get; set; }

    public DateTime? SabtDat { get; set; }

    public string? Comments { get; set; }

    public int? State { get; set; }

    public int? KarbarId { get; set; }
}