using Bonyan.Layer.Domain.Enumerations;

namespace Nezam.Modular.ESS.Secretariat.Domain.Documents;

public class DocumentStatus : Enumeration
{
    public static readonly DocumentStatus Draft = new DocumentStatus(0, nameof(Draft));
    public static readonly DocumentStatus Send = new DocumentStatus(1, nameof(Send));
    public static readonly DocumentStatus Archive = new DocumentStatus(1, nameof(Archive));
    public DocumentStatus(int id, string name) : base(id, name)
    {
    }
}