using Bonyan.Layer.Domain.Entities;
using Nezam.Modular.ESS.Identity.Domain.Shared.User;
using Nezam.Modular.ESS.Secretariat.Domain.Shared.Documents.ValueObjects;

namespace Nezam.Modular.ESS.Secretariat.Domain.Documents;

public class DocumentActivityLogEntity : BonEntity<DocumentActivityLogId>
{
    protected DocumentActivityLogEntity()
    {
    }

    public DocumentActivityLogEntity(DocumentId documentId, DateTime activityDate, UserId UserId, string key,
            string activityDescription)
        // Example of generating an identifier based on date
    {
        Id = DocumentActivityLogId.NewId();
        DocumentId = documentId;
        ActivityDate = activityDate;
        UserId = UserId;
        Description = activityDescription;
        Key = key;
    }

    public DateTime ActivityDate { get; private set; }
    public UserId UserId { get; private set; }
    public string Key { get; private set; }
    public string Description { get; private set; }

    public DocumentId DocumentId { get; set; }
}