namespace Nezam.ESS.backend.Data.Models;

public class TblSmsMessage
{
    public long MsgId { get; set; }

    public string? Message { get; set; }

    public int? KarbarId { get; set; }

    public bool? Status { get; set; }

    public DateTime? EnSabtDat { get; set; }

    public int? DNemayandegiCod { get; set; }

    public virtual ICollection<TblSmsNumber> TblSmsNumbers { get; set; } = new List<TblSmsNumber>();
}