namespace Nezam.ESS.backend.Data.Models;

public class TblAzayeSazmanLog
{
    /// <summary>
    ///     آی دی جدول
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    ///     آی دی مربوط به اعضای سازمان
    /// </summary>
    public int? OzveyatId { get; set; }

    /// <summary>
    ///     1-select
    ///     2-insert
    ///     3-update
    ///     4-delete
    /// </summary>
    public byte? Noa { get; set; }

    /// <summary>
    ///     0. جدید
    ///     1.ارسال شده
    ///     2.تایید شده
    /// </summary>
    public byte? Flag { get; set; }

    /// <summary>
    ///     تاریخ شمسی
    /// </summary>
    public string? Date { get; set; }

    /// <summary>
    ///     ساعت
    /// </summary>
    public string? Time { get; set; }

    /// <summary>
    ///     نام سرور ارسالی (می تواند خالی باشد)
    /// </summary>
    public string? Servername { get; set; }

    public string? LocalNetAddress { get; set; }

    public string? ClientNetAddress { get; set; }
}