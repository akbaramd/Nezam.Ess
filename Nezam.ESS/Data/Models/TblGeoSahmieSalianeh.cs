namespace Nezam.ESS.backend.Data.Models;

public class TblGeoSahmieSalianeh
{
    public long Id { get; set; }

    public long LegalMemberId { get; set; }

    public int Sal { get; set; }

    public int ScopeId { get; set; }

    public string? Comments { get; set; }

    public double? MetrajSalianehGeo { get; set; }

    public double? TedadSalianehGeo { get; set; }

    public double? ConcurrentGeo { get; set; }

    public double? MetrajSalianehBeton { get; set; }

    public double? TedadSalianehBeton { get; set; }

    public double? ConcurrentBeton { get; set; }

    public double? MetrajSalianehJoosh { get; set; }

    public double? TedadSalianehJoosh { get; set; }

    public double? ConcurrentJoosh { get; set; }
}