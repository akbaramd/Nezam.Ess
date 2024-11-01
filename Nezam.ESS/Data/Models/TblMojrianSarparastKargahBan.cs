namespace Nezam.ESS.backend.Data.Models;

public class TblMojrianSarparastKargahBan
{
    public int Id { get; set; }

    public long? OzviyatNo { get; set; }

    public int? State { get; set; }

    public string? Comments { get; set; }

    public virtual TblEngineer? OzviyatNoNavigation { get; set; }
}