using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nezam.EES.Service.Identity.Domains.Users;
using Nezam.EEs.Shared.Domain.Identity.User;
using Payeh.SharedKernel.EntityFrameworkCore.Domain;

namespace Nezam.EES.Service.Identity.Infrastructure.EntityFrameworkCore.Configurations;

public class UserEntityConfiguration : IEntityTypeConfiguration<UserEntity>
{
    public void Configure(EntityTypeBuilder<UserEntity> builder)
    {
        // Configure UserId as the primary key
        builder.HasKey(x => x.UserId);  // Explicitly telling EF that UserId is the primary key

        builder.Property(x => x.UserId).HasBusinessIdConversion();
        
        builder.Property(x => x.UserName).HasStringBusinessIdConversion();
        builder.HasIndex(x => x.UserName).IsUnique();
        

        builder.OwnsOne(x => x.Email, v =>
        {
            v.Property(x => x.Value).HasColumnName("Email");
        });

        builder.OwnsOne(x => x.Password, v =>
        {
            v.Property(x => x.Value).HasColumnName("Password");
        });

        builder.OwnsOne(x => x.Profile, v =>
        {
            v.Property(x => x.FirstName).HasColumnName("FirstName");
            v.Property(x => x.LastName).HasColumnName("LastName");
        });

        // Configure the many-to-many relationship between User and Role
        builder.HasMany(x => x.Roles)
            .WithMany(x => x.Users)
            .UsingEntity("UserRoles");
        
        // Configure the many-to-many relationship between User and Role
        builder.HasMany(x => x.Departments)
            .WithMany(x => x.Users)
            .UsingEntity("UserDepartments");
    }
}