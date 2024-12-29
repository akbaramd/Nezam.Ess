// UserEntityConfiguration.cs

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nezam.EES.Service.Identity.Domains.Roles;
using Payeh.SharedKernel.EntityFrameworkCore.Domain;

namespace Nezam.EES.Service.Identity.Infrastructure.EntityFrameworkCore.Configurations;

public class RoleEntityConfiguration : IEntityTypeConfiguration<RoleEntity>
{
    public void Configure(EntityTypeBuilder<RoleEntity> builder)
    {
        builder.HasKey(x => x.RoleId);
        builder.Property(x => x.RoleId).HasStringBusinessIdConversion();
    }
}