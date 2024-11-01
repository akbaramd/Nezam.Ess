namespace Nezam.ESS.backend.Data.Models;

public class TblSahmiyeSalianeOld
{
    public long Id { get; set; }

    public long OzviyatNo { get; set; }

    public int Sal { get; set; }

    public string? Comments { get; set; }

    public int? PayehNezarat { get; set; }

    public int? PayehTarrahi { get; set; }

    public long? Nezarat { get; set; }

    public long? Tarrahi { get; set; }

    public int? TNezarat { get; set; }

    public int? TTarrahi { get; set; }

    public string? SabtDate { get; set; }

    public int? CodReshteh { get; set; }

    public bool? FullTime { get; set; }

    public double? FullTimePercent { get; set; }
}