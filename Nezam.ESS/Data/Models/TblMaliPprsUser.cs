namespace Nezam.ESS.backend.Data.Models;

public class TblMaliPprsUser
{
    public int Id { get; set; }

    public int? DNemayandegiCod { get; set; }

    public string? Userpass { get; set; }

    public int? AccessLevel { get; set; }

    public string? Mac { get; set; }

    public string? Owner { get; set; }
}