namespace Nezam.ESS.backend.Data.Models;

public class TblLegalMembersActivityScope
{
    public int Id { get; set; }

    public int LegalMemberId { get; set; }

    public int ScopeId { get; set; }

    public int Master { get; set; }

    public string? Comments { get; set; }

    public int? Sal { get; set; }
}