namespace Nezam.ESS.backend.Data.Models;

public class TblBargMojrianMember
{
    public long Id { get; set; }

    public int? MojriCod { get; set; }

    public long? OzviyatNo { get; set; }

    public int? Active { get; set; }

    public DateTime? SabtDat { get; set; }

    public string? Comment { get; set; }

    public int? OzvType { get; set; }

    public virtual TblEngineer? OzviyatNoNavigation { get; set; }
}