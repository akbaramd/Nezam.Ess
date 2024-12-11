using System.ComponentModel.DataAnnotations;

namespace Nezam.ESS.backend.Data.Models;

public class TblUtmMaininfoTemp
{
    public int IdTemp { get; set; }
    public long? MainInfoId { get; set; }

    public DateTime? SabtDat { get; set; }

    public int? DNemayandegiCod { get; set; }
    public TblDafaterOstan? DNemayandegi { get; set; }

    [Display(Name = "نام خانوادگی")] public string? Fname { get; set; }

    [Display(Name = "نام")] public string? Name { get; set; }

    [Display(Name = "کد ملی")] public string? Mellicode { get; set; }

    [Display(Name = "شماره همراه")] public string? MobNo { get; set; }

    [Display(Name = "متراژ")]
    [Required(ErrorMessage = "{0} اجباری میباشد")]
    public double? Metraj { get; set; }

    [Display(Name = "آدرس")]
    [Required(ErrorMessage = "{0} اجباری میباشد")]
    public string? Address { get; set; }

    [Display(Name = "پلاک")] public string? PelakSabti { get; set; }

    [Display(Name = "منطقه")]
    [Required(ErrorMessage = "{0} اجباری میباشد")]
    public int? MantagehId { get; set; }

    public TblUtmMantageh? Mantageh { get; set; }

    public int RegUserId { get; set; }
    public int State { get; set; }
    public long? TrackingNumber { get; set; }

    public string StateToDisplay => State switch
    {
        0 => "منتظر پرداخت",
        1 => "پرداخت شده",
        _ => "مشخص نشده"
    };

    public bool HasMainInfo()
    {
        return MainInfoId != null;
    }
}