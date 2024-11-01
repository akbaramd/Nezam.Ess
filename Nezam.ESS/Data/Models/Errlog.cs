namespace Nezam.ESS.backend.Data.Models;

public class Errlog
{
    public Guid Id { get; set; }

    public DateTime? Logdate { get; set; }

    public string? Url { get; set; }

    public string? Errormessage { get; set; }

    public string? InnerException { get; set; }

    public string? ErrorSource { get; set; }

    public string? ErrorStackTrace { get; set; }

    public string? ErrorTargetSite { get; set; }

    public long Rowid { get; set; }
}