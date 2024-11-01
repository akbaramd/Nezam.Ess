namespace Nezam.ESS.backend.Data.Models;

public class TblKarbaranLogin
{
    public long Id { get; set; }

    public int? KarbarId { get; set; }

    public DateTime? Dat { get; set; }

    public string? MacAddress { get; set; }

    public int? LoginState { get; set; }
}