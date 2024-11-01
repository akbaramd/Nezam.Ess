namespace Nezam.ESS.backend.Data.Models;

public class TblMaininfoTail
{
    public long Id { get; set; }

    public long? BaseRadif { get; set; }

    public int? BaseCityId { get; set; }

    public long? TargetRadif { get; set; }

    public int? TargetCityId { get; set; }

    public int? ServiceType { get; set; }

    public int? ServiceField { get; set; }

    public int? MemberType { get; set; }

    public bool? State { get; set; }

    public DateTime? SabtDate { get; set; }
}