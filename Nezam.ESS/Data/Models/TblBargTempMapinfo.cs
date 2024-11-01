namespace Nezam.ESS.backend.Data.Models;

public class TblBargTempMapinfo
{
    public long Id { get; set; }

    public long TraceId { get; set; }

    public string? MalekFname { get; set; }

    public string? MalekName { get; set; }

    public string? MobNo { get; set; }

    public string? Address { get; set; }

    public DateTime? SabtDate { get; set; }

    public string? Comments { get; set; }

    public string? MelliCod { get; set; }

    public string? PelakSabti { get; set; }

    public string? BargTraceId { get; set; }

    public string? BranchInfo { get; set; }
}