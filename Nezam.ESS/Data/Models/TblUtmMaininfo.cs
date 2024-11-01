using System.ComponentModel.DataAnnotations;

namespace Nezam.ESS.backend.Data.Models;

public class TblUtmMaininfo
{
    public long Utminfoid { get; set; }

    [Required(ErrorMessage = "{0} اجباری میباشد")]
    [Display(Name = "نمایندگی")]
    public int? DNemayandegiCod { get; set; }

    public TblDafaterOstan? DNemayandegi { get; set; }

    public DateTime? SabtDat { get; set; }

    public string? Fname { get; set; }

    public string? Name { get; set; }

    public string? Mellicode { get; set; }

    public string? MobNo { get; set; }

    public double? Metraj { get; set; }

    public string? Address { get; set; }

    public string? PelakSabti { get; set; }

    public int? MantagehId { get; set; }
    public TblUtmMantageh? Mantageh { get; set; }

    public int? KarbarId { get; set; }

    public string? Comments { get; set; }

    public int? RdGeymatiId { get; set; }

    public int? State { get; set; }

    public string? ReceiveDat { get; set; }
}