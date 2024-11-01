namespace Nezam.ESS.backend.Data.Models;

public class TblSahmieh
{
    public int SabtNo { get; set; }

    public long OzviyatNo { get; set; }

    public int? GoroohCod { get; set; }

    public double? TedadCod { get; set; }

    public double? NTedad { get; set; }

    public double? TTedad { get; set; }

    public double? NezAlef { get; set; }

    public double? TarAlef { get; set; }

    public double? NezB { get; set; }

    public double? TarB { get; set; }

    public double? NezJa { get; set; }

    public double? TarJa { get; set; }

    public double? NezJb { get; set; }

    public double? TarJb { get; set; }

    public double? NezDa { get; set; }

    public double? TarDa { get; set; }

    public double? NezDb { get; set; }

    public double? TarDb { get; set; }

    public double? NezDc { get; set; }

    public double? TarDc { get; set; }

    public double? Nezarat { get; set; }

    public double? Tarrahi { get; set; }

    public string? DaftarFanniNo { get; set; }

    public double? TafavotMetraj { get; set; }

    public int Sal { get; set; }

    public DateTime? SabtDate { get; set; }

    public int? KarbarId { get; set; }

    public bool? NazerHamahang { get; set; }

    public int? WithErjaa { get; set; }

    public string? ErjaaDat { get; set; }

    public string? ErjaaTime { get; set; }

    public int CodReshteh { get; set; }

    public bool? TayidEmza { get; set; }

    public DateTime? TayidEmzaDat { get; set; }

    public int? MoshaverErjaa { get; set; }

    public string? Comments { get; set; }

    public long IdTemp { get; set; }

    public int? State { get; set; }

    public int? DNemayandegiCod { get; set; }

    public DateTime? CedoRegDateN { get; set; }

    public DateTime? CedoRegDateT { get; set; }

    public long? InvolvedMemberId { get; set; }

    public long? InvolvedMemberIdN { get; set; }

    public virtual TblEngineer OzviyatNoNavigation { get; set; } = null!;
}