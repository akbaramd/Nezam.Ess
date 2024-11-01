namespace Nezam.ESS.backend.Data.Models;

public class TblSmsNumber
{
    public long Id { get; set; }

    public long? MsgId { get; set; }

    public string? MobNo { get; set; }

    public long? OzviyatNo { get; set; }

    public bool? ErsalStatus { get; set; }

    public DateTime? ErsalDat { get; set; }

    public virtual TblSmsMessage? Msg { get; set; }
}