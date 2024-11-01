namespace Nezam.ESS.backend.Data.Models;

public class TblDafaterMember
{
    public long Id { get; set; }

    public int? DaftarNo { get; set; }

    public long? OzviyatNo { get; set; }

    public bool? Active { get; set; }

    public DateTime? ActiveDat { get; set; }

    public int? SalahiyatGoroohCod { get; set; }

    public bool? Manager { get; set; }

    /// <summary>
    ///     mojri or moshaver or daftar?
    /// </summary>
    public int? KarbariType { get; set; }

    public virtual TblEngineer? OzviyatNoNavigation { get; set; }
}