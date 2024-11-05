using Bonyan.Layer.Domain.Entities;
using System;

namespace Nezam.Modular.ESS.Secretariat.Domain.Documents;

public class DocumentAttachmentEntity : Entity<DocumentAttachmentId>
{
    public string FileName { get; private set; }
    public string FileType { get; private set; }
    public long FileSize { get; private set; }
    public string FilePath { get; private set; }
    public DateTime UploadDate { get; private set; }

    // Required for EF Core
    private DocumentAttachmentEntity() { }

    // Constructor
    public DocumentAttachmentEntity(string fileName, string fileType, long fileSize, string filePath)
    {
        FileName = fileName;
        FileType = fileType;
        FileSize = fileSize;
        FilePath = filePath;
        UploadDate = DateTime.UtcNow;
    }

    // Method to update file details if needed
    public void UpdateFileInfo(string newFileName, string newFileType, long newFileSize, string newFilePath)
    {
        FileName = newFileName;
        FileType = newFileType;
        FileSize = newFileSize;
        FilePath = newFilePath;
    }
}