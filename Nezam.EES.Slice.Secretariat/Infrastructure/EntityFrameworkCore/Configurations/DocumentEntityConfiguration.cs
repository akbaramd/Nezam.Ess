using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nezam.EES.Slice.Secretariat.Domains.Documents;
using Payeh.SharedKernel.EntityFrameworkCore.Domain;

namespace Nezam.EES.Slice.Secretariat.Infrastructure.EntityFrameworkCore.Configurations;

public class DocumentEntityConfiguration : IEntityTypeConfiguration<DocumentAggregateRoot>
{
    public void Configure(EntityTypeBuilder<DocumentAggregateRoot> builder)
    {
        
        builder.HasKey(x => x.DocumentId);

        builder.Property(x => x.DocumentId).HasBusinessIdConversion();

        builder.Property(x => x.Title).IsRequired().HasMaxLength(200);
        builder.Property(x => x.Content).IsRequired();

        builder.Property(x => x.OwnerParticipantId).IsRequired();
        builder.Property(x => x.ReceiverParticipantId).IsRequired();

        builder.Property(x => x.Status).HasEnumeration();
        builder.Property(x => x.Type).HasEnumeration();
        
        builder.HasOne(x => x.OwnerParticipant)
            .WithMany()
            .HasForeignKey(x => x.OwnerParticipantId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.ReceiverParticipant)
            .WithMany()
            .HasForeignKey(x => x.ReceiverParticipantId)
            .OnDelete(DeleteBehavior.Restrict);
        
        builder.Property(x => x.Type).IsRequired();
        builder.Property(x => x.Status).IsRequired();

        builder.HasMany(x => x.Attachments)
            .WithOne()
            .HasForeignKey(x => x.DocumentId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(x => x.Referrals)
            .WithOne()
            .HasForeignKey(x => x.DocumentId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}