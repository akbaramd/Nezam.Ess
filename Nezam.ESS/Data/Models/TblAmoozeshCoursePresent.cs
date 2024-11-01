namespace Nezam.ESS.backend.Data.Models;

public class TblAmoozeshCoursePresent
{
    public long Radif { get; set; }

    public long OzviyatNo { get; set; }

    public int CourseId { get; set; }

    public long CourseTimesId { get; set; }

    public DateTime? SabtDat { get; set; }

    public int? KarbarId { get; set; }

    public virtual TblEngineer OzviyatNoNavigation { get; set; } = null!;
}