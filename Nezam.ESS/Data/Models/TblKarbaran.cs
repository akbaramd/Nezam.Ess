namespace Nezam.ESS.backend.Data.Models;

public class TblKarbaran
{
    public int KarbarId { get; set; }

    public string? Kname { get; set; }

    public string? Pwd { get; set; }

    public bool? Active { get; set; }

    public string? Comment { get; set; }

    public byte[]? Img { get; set; }

    public bool? IsPersonel { get; set; }
}