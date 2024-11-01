namespace Nezam.ESS.backend.Data.Models;

public class TblMaliVamRequest
{
    public long Id { get; set; }

    public long? OzviyatNo { get; set; }

    public int? TashilatCod { get; set; }

    public DateTime? SabtDat { get; set; }

    /// <summary>
    ///     state=1 req- state=0 cancel -state=2 sent to bank
    /// </summary>
    public int? State { get; set; }

    public DateTime? RejectDat { get; set; }

    public DateTime? TayidDat { get; set; }

    public int? TayidKarbarId { get; set; }

    public int? RdErsal { get; set; }

    public int? BankCod { get; set; }

    public virtual TblEngineer? OzviyatNoNavigation { get; set; }
}