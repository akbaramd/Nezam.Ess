namespace Nezam.ESS.backend.Data.Models;

public class ZtblMaliPerson
{
    public int CodPerson { get; set; }

    public long? OzviyatNo { get; set; }

    public string? Title { get; set; }

    public virtual TblEngineer? OzviyatNoNavigation { get; set; }
}