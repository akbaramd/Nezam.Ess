using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nezam.Modular.ESS.Secretariat.Domain.Documents;

namespace Nezam.Modular.ESS.Infrastructure.Data.Configurations;

public class DocumentAggregateRootConfiguration : IEntityTypeConfiguration<DocumentAggregateRoot>
{
    public void Configure(EntityTypeBuilder<DocumentAggregateRoot> builder)
    {
        builder.HasKey(d => d.Id);

        builder.Property(d => d.Title)
            .IsRequired()
            .HasMaxLength(255);

        builder.Property(d => d.Content)
            .IsRequired();


        builder.HasMany(d => d.Attachments)
            .WithOne()
            .HasForeignKey(a => a.DocumentId);

        builder.HasMany(d => d.Referrals)
            .WithOne()
            .HasForeignKey(r => r.DocumentId);

        builder.HasMany(d => d.ActivityLogs)
            .WithOne()
            .HasForeignKey(a => a.DocumentId);

        builder.ToTable("Documents");
    }
}