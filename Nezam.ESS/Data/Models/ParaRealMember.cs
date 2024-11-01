namespace Nezam.ESS.backend.Data.Models;

public class ParaRealMember
{
    public int MemberType { get; set; }

    public int SystemCode { get; set; }

    public long MembershipCode { get; set; }

    public DateTime? MembershipDate { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? NationalCode { get; set; }

    public DateTime? BirthDate { get; set; }

    public string? BirthCertificateNumber { get; set; }

    public int? BirthCertificateCityId { get; set; }

    public int ActiveType { get; set; }

    public bool? Gender { get; set; }

    public string? FatherName { get; set; }
}