namespace Nezam.ESS.backend.Data.Models;

public class TblSetting
{
    public int SettingId { get; set; }

    public string? SettingDsc { get; set; }

    public int? SettingValInt { get; set; }

    public double? SettingValFloat { get; set; }

    public string? SettingValchar { get; set; }
}