namespace Nezam.ESS.backend.Data.Models;

public class ParaLegalMember
{
    public int MemberType { get; set; }

    public long SystemCode { get; set; }

    public long MembershipCode { get; set; }

    public DateTime? MembershipDate { get; set; }

    public string? Title { get; set; }

    public string? RegisterationCode { get; set; }

    public DateTime? RegisterationDate { get; set; }

    public int? ActiveType { get; set; }
}