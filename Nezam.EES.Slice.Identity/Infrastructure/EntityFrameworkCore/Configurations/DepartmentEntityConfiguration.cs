// UserEntityConfiguration.cs

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nezam.EES.Service.Identity.Domains.Departments;
using Nezam.EES.Service.Identity.Domains.Roles;
using Payeh.SharedKernel.EntityFrameworkCore.Domain;

namespace Nezam.EES.Service.Identity.Infrastructure.EntityFrameworkCore.Configurations;

public class DepartmentEntityConfiguration : IEntityTypeConfiguration<DepartmentEntity>
{
    public void Configure(EntityTypeBuilder<DepartmentEntity> builder)
    {
        builder.HasKey(x => x.DepartmentId);
        builder.Property(x => x.DepartmentId).HasBusinessIdConversion();
    }
}