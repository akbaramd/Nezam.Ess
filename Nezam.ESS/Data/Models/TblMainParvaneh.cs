namespace Nezam.ESS.backend.Data.Models;

public class TblMainParvaneh
{
    public long ParvanehNo { get; set; }

    public long? OzviyatNo { get; set; }

    public string? ParvanehSerial { get; set; }

    public string? FirstSabtDat { get; set; }

    public string? Religion { get; set; }

    public int? Milit { get; set; }

    public int? ParvanehOstan { get; set; }

    public string? FSodoorDat { get; set; }

    public string? FSodoorRooz { get; set; }

    public string? FSodoorMah { get; set; }

    public string? FSodoorSal { get; set; }

    public string? ExpDat { get; set; }

    public string? ExpRooz { get; set; }

    public string? ExpMah { get; set; }

    public string? ExpSal { get; set; }

    public int? Active { get; set; }

    public int? Payeh { get; set; }

    public string? TamdidDat { get; set; }

    public string? TamdidRooz { get; set; }

    public string? TamdidMah { get; set; }

    public string? TamdidSal { get; set; }

    public long? Fish { get; set; }

    public string? Dfish { get; set; }

    public int? Mojri { get; set; }

    public string? MojriDate { get; set; }

    public bool? Gas { get; set; }

    public string? GasDate { get; set; }

    public int? Joosh { get; set; }

    public string? Jooshdate { get; set; }

    public long? MelliCod { get; set; }

    public string? Comments { get; set; }

    public bool? GasTookarCert { get; set; }

    public virtual TblEngineer? OzviyatNoNavigation { get; set; }
}