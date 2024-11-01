namespace Nezam.ESS.backend.Data.Models;

public class TblMaliBankTransState
{
    public long Id { get; set; }

    public long? FishNo { get; set; }

    public long? SeriFish { get; set; }

    public DateTime? Dat { get; set; }

    /// <summary>
    ///     1= odat
    /// </summary>
    public int? State { get; set; }

    public string? MerchatId { get; set; }
}