namespace Nezam.ESS.backend.Data.Models;

public class TblTafkikMaininfo
{
    public long Id { get; set; }

    public int TafkikSabtNo { get; set; }

    public int Sal { get; set; }

    public string? SabtDat { get; set; }

    public DateTime? EnSabtDat { get; set; }

    public int? MapIdNezam { get; set; }

    public int? MapIdSalNezam { get; set; }

    public string? MalekName { get; set; }

    public string? MalekFname { get; set; }

    public string? MalekMobNo { get; set; }

    public string? MalekAddress { get; set; }

    public string? PelakSabti { get; set; }

    public string? ParvanehNo { get; set; }

    public string? ParvanehDat { get; set; }

    public string? PayankarNo { get; set; }

    public string? PayankarDat { get; set; }

    public int? MalekMetraj { get; set; }

    public int? MalekTabage { get; set; }

    public int? Metraj { get; set; }

    public int? TedadTabage { get; set; }

    public string? Comments { get; set; }

    public int? RdGeymati { get; set; }

    public int? StatusCod { get; set; }

    public int? TedadVahed { get; set; }

    public string? MalekMellicode { get; set; }

    public double? MablagMap { get; set; }

    public bool? Tejari { get; set; }

    public double? TraceId { get; set; }

    public string? MapRecDat { get; set; }

    public int? SamaneSabtNo { get; set; }
}