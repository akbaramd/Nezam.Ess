namespace Nezam.ESS.backend.Data.Models;

public class TblErjaaNezaratMapSpool
{
    public long Id { get; set; }

    public int MapId { get; set; }

    public int Sal { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public int? Counter { get; set; }

    public int CodReshteh { get; set; }

    public int? StateCod { get; set; }

    public int KarbarId { get; set; }

    public string? Comment { get; set; }

    public double? TedadKar { get; set; }

    public int? ScoreState { get; set; }

    public int? DNemayandegiCod { get; set; }
}