namespace Nezam.ESS.backend.Data.Models;

public class TblSahmiyeSaliane
{
    public long Id { get; set; }

    public long OzviyatNo { get; set; }

    public int Sal { get; set; }

    public string? Comments { get; set; }

    public int? PayehNezarat { get; set; }

    public int? PayehTarrahi { get; set; }

    public long? Nezarat { get; set; }

    public long? Tarrahi { get; set; }

    public double? TNezarat { get; set; }

    public double? TTarrahi { get; set; }

    public string? SabtDate { get; set; }

    public int? CodReshteh { get; set; }

    public bool? FullTime { get; set; }

    public double? FullTimePercent { get; set; }

    public int? ZaribDefined { get; set; }

    public int? DNemayandegiCod { get; set; }

    public long? ExtNezarat { get; set; }

    public long? ExtTarrahi { get; set; }

    public double? ExtTNezarat { get; set; }

    public double? ExtTTarrahi { get; set; }

    public virtual TblEngineer OzviyatNoNavigation { get; set; } = null!;
}