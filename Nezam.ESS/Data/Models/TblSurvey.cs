namespace Nezam.ESS.backend.Data.Models;

public class TblSurvey
{
    public int Id { get; set; }

    public long? OzviyatNo { get; set; }

    public byte? Q1 { get; set; }

    public byte? Q2 { get; set; }

    public byte? Q3 { get; set; }

    public byte? Q4 { get; set; }

    public byte? Q5 { get; set; }

    public byte? Q6 { get; set; }

    public byte? Q7 { get; set; }

    public byte? Q8 { get; set; }

    public byte? Q9 { get; set; }

    public byte? Q10 { get; set; }

    public byte? Q11 { get; set; }

    public byte? Q12 { get; set; }

    public string? Q13 { get; set; }

    public string? Q14 { get; set; }

    public string? Q15 { get; set; }

    public DateTime? Dfill { get; set; }

    public virtual TblEngineer? OzviyatNoNavigation { get; set; }
}