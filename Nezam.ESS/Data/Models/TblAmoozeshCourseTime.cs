namespace Nezam.ESS.backend.Data.Models;

public class TblAmoozeshCourseTime
{
    public long Id { get; set; }

    public int? CourseId { get; set; }

    public string? Dat { get; set; }

    public string? StartTime { get; set; }

    public string? EndTime { get; set; }
}