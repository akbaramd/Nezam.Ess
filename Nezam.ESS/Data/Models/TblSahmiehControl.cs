namespace Nezam.ESS.backend.Data.Models;

public class TblSahmiehControl
{
    public long Id { get; set; }

    public long? OzviyatNo { get; set; }

    public int? MapId { get; set; }

    public int? MetrajControl { get; set; }

    public string? Name { get; set; }

    public string? Fname { get; set; }

    public int? CodReshteh { get; set; }

    public int? Sal { get; set; }

    public int? Mah { get; set; }

    public int? Rooz { get; set; }

    public string? Reshteh { get; set; }

    public int? EngId { get; set; }

    public string? Dat { get; set; }

    public int? StatusCod { get; set; }

    public string? Comments { get; set; }

    public DateTime? EnDat { get; set; }

    public int? DNemayandegiCod { get; set; }

    public virtual TblEngineer? OzviyatNoNavigation { get; set; }
}