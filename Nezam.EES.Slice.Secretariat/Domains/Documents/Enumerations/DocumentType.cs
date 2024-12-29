using Payeh.SharedKernel.Domain.Enumerations;

namespace Nezam.EES.Slice.Secretariat.Domains.Documents.Enumerations;

public class DocumentType : Enumeration
{
    public static readonly DocumentType Outgoing = new DocumentType(0, nameof(Outgoing));
    public static readonly DocumentType Incoming = new DocumentType(1, nameof(Incoming));
    public static readonly DocumentType Internal = new DocumentType(2, nameof(Internal));
    public DocumentType(int id, string name) : base(id, name)
    {
    }
}