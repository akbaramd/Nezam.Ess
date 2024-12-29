using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nezam.EES.Slice.Secretariat.Domains.Participant;
using Payeh.SharedKernel.EntityFrameworkCore.Domain;

namespace Nezam.EES.Slice.Secretariat.Infrastructure.EntityFrameworkCore.Configurations;

public class ParticipantEntityConfiguration : IEntityTypeConfiguration<Participant>
{
    public void Configure(EntityTypeBuilder<Participant> builder)
    {
        builder.HasKey(x => x.ParticipantId);

        builder.Property(x => x.ParticipantId).HasBusinessIdConversion();
        builder.Property(x => x.UserId).HasBusinessIdConversion();
        builder.Property(x => x.Name).IsRequired().HasMaxLength(100);
    }
}