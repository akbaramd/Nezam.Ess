using Payeh.SharedKernel.Domain.Enumerations;

namespace Nezam.EES.Slice.Secretariat.Domains.Documents.Enumerations;

public class DocumentType : Enumeration
{
    public static readonly DocumentType Internal = new DocumentType(0, nameof(Internal));
    public static readonly DocumentType FalseReport = new DocumentType(1, nameof(FalseReport));
    public DocumentType(int id, string name) : base(id, name)
    {
    }
}