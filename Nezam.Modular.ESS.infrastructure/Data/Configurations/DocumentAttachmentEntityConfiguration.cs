using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nezam.Modular.ESS.Secretariat.Domain.Documents;

namespace Nezam.Modular.ESS.Infrastructure.Data.Configurations
{
    public class DocumentAttachmentEntityConfiguration : IEntityTypeConfiguration<DocumentAttachmentEntity>
    {
        public void Configure(EntityTypeBuilder<DocumentAttachmentEntity> builder)
        {
            builder.HasKey(a => a.Id);
            // Configure properties for DocumentAttachmentEntity
        }
    }
}