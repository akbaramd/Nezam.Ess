namespace Nezam.ESS.backend.Data.Models;

public class TblBargSendInfo
{
    public long Id { get; set; }

    public int? BargMapId { get; set; }

    public long? NezamTraceId { get; set; }

    public string? BarghTraceId { get; set; }

    public DateTime? SabtDate { get; set; }

    public int? Tayid { get; set; }

    public double? OhmHambandi { get; set; }

    public double? OhmNonHambandi { get; set; }

    public int? ResultHarim { get; set; }

    public string? Comments { get; set; }
}