// DocumentAggregateRootConfiguration.cs
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nezam.Modular.ESS.Secretariat.Domain.Documents;

public class DocumentAggregateRootConfiguration : IEntityTypeConfiguration<DocumentAggregateRoot>
{
    public void Configure(EntityTypeBuilder<DocumentAggregateRoot> builder)
    {
        builder.HasMany(d => d.Attachments)
            .WithOne()
            .HasForeignKey(a => a.DocumentId);

        builder.HasMany(d => d.Referrals)
            .WithOne()
            .HasForeignKey(r => r.DocumentId);

        builder.HasMany(d => d.ActivityLogs)
            .WithOne()
            .HasForeignKey(a => a.DocumentId);

        builder.HasOne(d => d.OwnerUser)
            .WithMany()
            .HasForeignKey(a => a.OwnerUserId);
    }
}