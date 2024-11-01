namespace Nezam.ESS.backend.Data.Models;

public class TblEngineersSiteLog
{
    public long Id { get; set; }

    public DateTime? Dat { get; set; }

    public long? OzviyatNo { get; set; }

    public string? MobNo { get; set; }

    public string? Address { get; set; }

    public string? JobAddress { get; set; }

    public string? Tel { get; set; }

    public string? JobTel { get; set; }

    public string? CodPostiManzel { get; set; }

    public string? CodPostiJob { get; set; }

    public string? EMail { get; set; }

    public string? Website { get; set; }

    public string? MelliCod { get; set; }

    public virtual TblEngineer? OzviyatNoNavigation { get; set; }
}