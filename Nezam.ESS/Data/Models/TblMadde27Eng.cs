namespace Nezam.ESS.backend.Data.Models;

public class TblMadde27Eng
{
    public long Id { get; set; }

    public long? OzviyatNo { get; set; }

    public bool? Active { get; set; }

    public DateTime? SabtDat { get; set; }

    public int? Term { get; set; }

    public bool? PelleFararCert { get; set; }

    public string? Comments { get; set; }

    public string? ParvanehExpDat { get; set; }

    public bool? ActiveStat { get; set; }

    public virtual TblEngineer? OzviyatNoNavigation { get; set; }
}