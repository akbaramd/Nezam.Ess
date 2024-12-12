using Payeh.SharedKernel.Domain.Enumerations;

namespace Nezam.Modular.ESS.Secretariat.Domain.Shared.Documents.Enumerations;

public class DocumentStatus : Enumeration
{
    public static readonly DocumentStatus Draft = new DocumentStatus(0, nameof(Draft));
    public static readonly DocumentStatus Published = new DocumentStatus(1, nameof(Published));
    public static readonly DocumentStatus Archive = new DocumentStatus(2, nameof(Archive));
    public DocumentStatus(int id, string name) : base(id, name)
    {
    }
}