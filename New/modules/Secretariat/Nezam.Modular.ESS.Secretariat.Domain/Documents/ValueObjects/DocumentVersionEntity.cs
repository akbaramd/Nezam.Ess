using Bonyan.Layer.Domain.Entities;
using Bonyan.Layer.Domain.Enumerations;
using Bonyan.UserManagement.Domain.ValueObjects;

namespace Nezam.Modular.ESS.Secretariat.Domain.Documents.ValueObjects;

// New enumeration class inheriting from Enumeration base class
public class DocumentApprovalStatus : Enumeration
{
    public static readonly DocumentApprovalStatus Pending = new DocumentApprovalStatus(1, "Pending");
    public static readonly DocumentApprovalStatus Approved = new DocumentApprovalStatus(2, "Approved");
    public static readonly DocumentApprovalStatus Finalized = new DocumentApprovalStatus(3, "Finalized");

    private DocumentApprovalStatus(int id, string name) : base(id, name) { }
}

// Supporting entities inheriting from Entity base class
public class DocumentVersion : Entity<DocumentVersionId>
{
    public int VersionNumber { get; private set; }
    public DateTime VersionDate { get; private set; }
    public UserId EditorId { get; private set; }
    public string ContentSnapshot { get; private set; }
    public string TitleSnapshot { get; set; }

    public DocumentVersion(int versionNumber, DateTime versionDate, UserId editorId, string titleSnapshot, string contentSnapshot)
         // Assuming base entity has an identifier setup with the version number
    {
        Id = DocumentVersionId.CreateNew();
        VersionDate = versionDate;
        EditorId = editorId;
        ContentSnapshot = contentSnapshot;
        TitleSnapshot = titleSnapshot;
    }
}

public class DocumentActivityLog : Entity<DocumentActivityLogId>
{
    public DateTime ActivityDate { get; private set; }
    public UserId UserId { get; private set; }
    public string ActivityDescription { get; private set; }

    public DocumentActivityLog(DateTime activityDate, UserId userId, string activityDescription)
         // Example of generating an identifier based on date
    {
        Id = DocumentActivityLogId.CreateNew();
        ActivityDate = activityDate;
        UserId = userId;
        ActivityDescription = activityDescription;
    }
}