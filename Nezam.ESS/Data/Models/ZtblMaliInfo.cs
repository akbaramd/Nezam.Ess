namespace Nezam.ESS.backend.Data.Models;

public class ZtblMaliInfo
{
    public long Id { get; set; }

    public int? SabtNo { get; set; }

    public int? Sal { get; set; }

    public int? RdGeymati { get; set; }

    public int? CodPerson { get; set; }

    /// <summary>
    ///     ztbl_mali_khadamat_type
    /// </summary>
    public int? KhadamatType { get; set; }

    public int? KhadamatDesc { get; set; }

    public double? MetrajMali { get; set; }

    public double? PercMablag { get; set; }

    public double? Fee { get; set; }

    public double? KollMablag { get; set; }

    public string? HesabMagsad { get; set; }

    public int? DaftarTarrahiNo { get; set; }

    public int? TayidFishType { get; set; }

    public int? KarbarIdSabt { get; set; }

    public int? KarbarIdTayid { get; set; }

    public DateTime? TayidDate { get; set; }

    public string? Comment { get; set; }
}