using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nezam.EES.Slice.Secretariat.Domains.Documents;
using Payeh.SharedKernel.EntityFrameworkCore.Domain;

namespace Nezam.EES.Slice.Secretariat.Infrastructure.EntityFrameworkCore.Configurations;

public class DocumentAttachmentEntityConfiguration : IEntityTypeConfiguration<DocumentAttachmentEntity>
{
    public void Configure(EntityTypeBuilder<DocumentAttachmentEntity> builder)
    {
        builder.HasKey(x => x.DocumentAttachmentId);

        builder.Property(x => x.DocumentAttachmentId).HasBusinessIdConversion();

        builder.Property(x => x.FileName).IsRequired().HasMaxLength(255);
        builder.Property(x => x.FileType).IsRequired().HasMaxLength(50);
        builder.Property(x => x.FileSize).IsRequired();
        builder.Property(x => x.FilePath).IsRequired().HasMaxLength(500);

        builder.Property(x => x.UploadDate).IsRequired();
    }
}