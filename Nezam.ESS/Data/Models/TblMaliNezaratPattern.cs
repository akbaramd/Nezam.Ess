namespace Nezam.ESS.backend.Data.Models;

public class TblMaliNezaratPattern
{
    public int Sal { get; set; }

    public int RdGeymati { get; set; }

    /// <summary>
    ///     كد رشته 99 براي ناظر هماهنگ كننده مي باشد
    /// </summary>
    public int CodReshteh { get; set; }

    public int GoroohCod { get; set; }

    public double? SahmReshteh { get; set; }

    public int? Stage1 { get; set; }

    public int? Stage2 { get; set; }

    public int? Stage3 { get; set; }

    public int? PayanKar { get; set; }

    public int? MaliyatPercent { get; set; }

    public string? Comments { get; set; }
}