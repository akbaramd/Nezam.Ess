namespace Nezam.ESS.backend.Data.Models;

public class TblElection
{
    public int Id { get; set; }

    public long? OzviyatNo { get; set; }

    public DateTime? Dsabt { get; set; }

    public int? ElectionId { get; set; }

    public virtual TblEngineer? OzviyatNoNavigation { get; set; }
}