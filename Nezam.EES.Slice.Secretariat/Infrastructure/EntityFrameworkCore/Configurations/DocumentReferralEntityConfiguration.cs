using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nezam.EES.Slice.Secretariat.Domains.Documents;
using Payeh.SharedKernel.EntityFrameworkCore.Domain;

namespace Nezam.EES.Slice.Secretariat.Infrastructure.EntityFrameworkCore.Configurations;

public class DocumentReferralEntityConfiguration : IEntityTypeConfiguration<DocumentReferralEntity>
{
    public void Configure(EntityTypeBuilder<DocumentReferralEntity> builder)
    {
        builder.HasKey(x => x.DocumentReferralId);

        builder.Property(x => x.DocumentReferralId).HasBusinessIdConversion();

        builder.Property(x => x.ReferralDate).IsRequired();
        builder.Property(x => x.Status).IsRequired();

        builder.Property(x => x.ResponseContent).IsRequired(false).HasMaxLength(1000);
        builder.Property(x => x.Status).HasEnumeration();
        
        builder.HasOne(x => x.ReferrerParticipant)
            .WithMany()
            .HasForeignKey(x => x.ReferrerParticipantId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.ReceiverParticipant)
            .WithMany()
            .HasForeignKey(x => x.ReceiverParticipantId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne<DocumentReferralEntity>()
            .WithMany()
            .HasForeignKey(x => x.ParentReferralId)
            .OnDelete(DeleteBehavior.Restrict); 
        

    }
}