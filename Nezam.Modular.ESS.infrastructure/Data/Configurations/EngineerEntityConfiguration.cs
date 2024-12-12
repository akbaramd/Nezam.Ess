// EngineerEntityConfiguration.cs

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nezam.Modular.ESS.Identity.Domain.Engineer;
using Nezam.Modular.ESS.Identity.Domain.User;

namespace Nezam.Modular.ESS.Infrastructure.Data.Configurations;

public class EngineerEntityConfiguration : IEntityTypeConfiguration<EngineerEntity>
{
    public void Configure(EntityTypeBuilder<EngineerEntity> builder)
    {
        builder.HasBaseType<UserEntity>()
            .ToTable("Engineers");

        builder.Property(e => e.EngineerId)
            .HasColumnName("EngineerId");
    }
}