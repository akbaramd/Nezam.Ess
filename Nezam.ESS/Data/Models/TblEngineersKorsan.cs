namespace Nezam.ESS.backend.Data.Models;

public class TblEngineersKorsan
{
    public int Id { get; set; }

    public long OzviyatNo { get; set; }

    public int? KorsanCod { get; set; }

    public string? Comments { get; set; }

    public DateTime? InsertDate { get; set; }

    public int? InsertKarbarId { get; set; }

    public DateTime? ModifiyDate { get; set; }

    public int? ModifyKarbarId { get; set; }

    public bool? Active { get; set; }

    public virtual TblEngineer OzviyatNoNavigation { get; set; } = null!;
}