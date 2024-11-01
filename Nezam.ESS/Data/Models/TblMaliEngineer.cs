namespace Nezam.ESS.backend.Data.Models;

public class TblMaliEngineer
{
    public long OzviyatNo { get; set; }

    public long MaliTafsili { get; set; }

    public bool? BankSodoor { get; set; }

    public virtual TblEngineer OzviyatNoNavigation { get; set; } = null!;
}