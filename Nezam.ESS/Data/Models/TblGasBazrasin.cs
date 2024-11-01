namespace Nezam.ESS.backend.Data.Models;

public class TblGasBazrasin
{
    public int BazrasCod { get; set; }

    public long OzviyatNo { get; set; }

    public string? Bazrasname { get; set; }

    public string? Tel { get; set; }

    public bool? Active { get; set; }

    public string? ParvanehNo { get; set; }

    public string? EtebarDate { get; set; }

    public int? Payeh { get; set; }

    public int? Term { get; set; }

    public int? MomayeziTerm { get; set; }

    public string? SabtDat { get; set; }

    public int? BankCod { get; set; }

    public bool? MehrBazras { get; set; }

    public bool? TookarCert { get; set; }

    public string? Comments { get; set; }

    public bool? ActiveTemp { get; set; }

    public virtual TblEngineer OzviyatNoNavigation { get; set; } = null!;
}