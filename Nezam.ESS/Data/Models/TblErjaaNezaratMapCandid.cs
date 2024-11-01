namespace Nezam.ESS.backend.Data.Models;

public class TblErjaaNezaratMapCandid
{
    public long Id { get; set; }

    public long? MapSpoolId { get; set; }

    public DateTime? SabtDat { get; set; }

    public long? OzviyatNo { get; set; }

    public double? Score { get; set; }

    public double? ScoreOnCandidTime { get; set; }

    public bool? State { get; set; }

    public DateTime? RejectDat { get; set; }

    public bool? Winner { get; set; }

    public DateTime? WinDat { get; set; }

    public string? Comments { get; set; }

    public virtual TblEngineer? OzviyatNoNavigation { get; set; }
}