namespace Nezam.ESS.backend.Data.Models;

public class TblMaliBankTransaction
{
    public long Id { get; set; }

    public DateTime? SabtDat { get; set; }

    /// <summary>
    ///     REFERENCENUMBER
    /// </summary>
    public long? FishNo { get; set; }

    public long? Amount { get; set; }

    /// <summary>
    ///     PAYMENTID
    /// </summary>
    public long? SeriFish { get; set; }

    /// <summary>
    ///     invoceNo
    /// </summary>
    public long? CustomerId { get; set; }

    public string? MerchantId { get; set; }

    public string? Comment { get; set; }
}