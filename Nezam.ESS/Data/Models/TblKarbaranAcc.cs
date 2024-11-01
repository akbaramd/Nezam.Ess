namespace Nezam.ESS.backend.Data.Models;

public class TblKarbaranAcc
{
    public int Id { get; set; }

    public int? KarbarId { get; set; }

    public byte? AccessRole { get; set; }

    public byte? AccessLevel { get; set; }
}