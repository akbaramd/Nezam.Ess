namespace Nezam.ESS.backend.Data.Models;

public class TblCommunityType
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public string? ReportFilename1 { get; set; }

    public string? ReportFilename2 { get; set; }

    public bool? BedehiCheck { get; set; }
}