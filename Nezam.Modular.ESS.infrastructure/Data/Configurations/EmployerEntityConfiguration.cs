// EmployerEntityConfiguration.cs
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nezam.Modular.ESS.Identity.Domain.Employer;
using Nezam.Modular.ESS.Identity.Domain.User;

public class EmployerEntityConfiguration : IEntityTypeConfiguration<EmployerEntity>
{
    public void Configure(EntityTypeBuilder<EmployerEntity> builder)
    {
        builder.HasBaseType<UserEntity>()
            .ToTable("Employers");

        builder.Property(e => e.EmployerId)
            .HasColumnName("EmployerId");
    }
}