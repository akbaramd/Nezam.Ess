using Bonyan.Layer.Domain.Entities;
using Bonyan.UserManagement.Domain.ValueObjects;
using Nezam.Modular.ESS.Secretariat.Domain.Documents.ValueObjects;

namespace Nezam.Modular.ESS.Secretariat.Domain.Documents;

public class DocumentActivityLogEntity : Entity<DocumentActivityLogId>
{
    public DateTime ActivityDate { get; private set; }
    public UserId UserId { get; private set; }
    public string Key { get; private set; }
    public string Description { get; private set; }
    
    public DocumentId DocumentId { get; set; }

    protected DocumentActivityLogEntity()
    {
        
    }
    public DocumentActivityLogEntity(DocumentId documentId,DateTime activityDate, UserId userId, string key, string activityDescription)
        // Example of generating an identifier based on date
    {
        Id = DocumentActivityLogId.CreateNew();
        DocumentId = documentId;
        ActivityDate = activityDate;
        UserId = userId;
        Description = activityDescription;
        Key = key;
    }
}