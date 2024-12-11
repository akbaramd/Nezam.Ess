using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nezam.Modular.ESS.Identity.Domain.User;

public class UserEntityConfiguration : IEntityTypeConfiguration<UserEntity>
{
    public void Configure(EntityTypeBuilder<UserEntity> builder)
    {
        // Configure UserId as the primary key
        builder.HasKey(x => x.UserId);  // Explicitly telling EF that UserId is the primary key

        // Configuring value objects as owned types
        builder.OwnsOne(x => x.UserName, userName =>
        {
            userName.Property(x => x.Value).HasColumnName("UserName");
        });

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
            v.Property(x => x.Avatar).HasColumnName("Avatar");
            v.Property(x => x.FirstName).HasColumnName("FirstName");
            v.Property(x => x.LastName).HasColumnName("LastName");
        });

        // Configure the many-to-many relationship between User and Role
        builder.HasMany(x => x.Roles)
            .WithMany(x => x.Users)
            .UsingEntity("UserRoles");
    }
}