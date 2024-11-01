namespace Nezam.ESS.backend.Data.Models;

public class TblMoshaverinPayeh
{
    public int Id { get; set; }

    public int? MoshaverId { get; set; }

    public int? CodReshteh { get; set; }

    public int? PnezaratCod { get; set; }

    public int? PtarrahiCod { get; set; }

    public int? PejraCod { get; set; }
}