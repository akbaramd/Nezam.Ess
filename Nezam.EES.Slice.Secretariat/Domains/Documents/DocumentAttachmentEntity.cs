using Nezam.EES.Slice.Secretariat.Domains.Documents.ValueObjects;
using Payeh.SharedKernel.Domain;

namespace Nezam.EES.Slice.Secretariat.Domains.Documents
{
    public class DocumentAttachmentEntity : Entity
    {
        public DocumentAttachmentId DocumentAttachmentId { get; set; }
        public string FileName { get; private set; }
        public string FileType { get; private set; }
        public long FileSize { get; private set; }
        public string FilePath { get; private set; }
        public DateTime UploadDate { get; private set; }

        public DocumentId DocumentId { get;private set; }
        
        // Required for EF Core
        private DocumentAttachmentEntity() { }

        // Constructor with validation
        public DocumentAttachmentEntity(DocumentId documentId,string fileName, string fileType, long fileSize, string filePath)
        {
            DocumentAttachmentId = DocumentAttachmentId.NewId();
            DocumentId = documentId;
            SetFileName(fileName);
            SetFileType(fileType);
            SetFileSize(fileSize);
            SetFilePath(filePath);
            UploadDate = DateTime.UtcNow;
        }

        // Method to update file details with validation
        public void UpdateFileInfo(string newFileName, string newFileType, long newFileSize, string newFilePath)
        {
            SetFileName(newFileName);
            SetFileType(newFileType);
            SetFileSize(newFileSize);
            SetFilePath(newFilePath);
        }

        // Private helper methods for validation
        private void SetFileName(string fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName))
                throw new ArgumentException("File name cannot be empty or whitespace.", nameof(fileName));

            FileName = fileName;
        }

        private void SetFileType(string fileType)
        {
            if (string.IsNullOrWhiteSpace(fileType))
                throw new ArgumentException("File type cannot be empty or whitespace.", nameof(fileType));

            FileType = fileType;
        }

        private void SetFileSize(long fileSize)
        {
            if (fileSize <= 0)
                throw new ArgumentException("File size must be a positive number.", nameof(fileSize));

            FileSize = fileSize;
        }

        private void SetFilePath(string filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath))
                throw new ArgumentException("File path cannot be empty or whitespace.", nameof(filePath));

            FilePath = filePath;
        }

        public override object GetKey()
        {
            return DocumentAttachmentId;
        }
    }
}
