using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nezam.EES.Service.Identity.Domains.Users;
using Payeh.SharedKernel.EntityFrameworkCore.Domain;

namespace Nezam.EES.Service.Identity.Infrastructure.EntityFrameworkCore.Configurations;

public class UserTokenEntityConfiguration : IEntityTypeConfiguration<UserTokenEntity>
{
    public void Configure(EntityTypeBuilder<UserTokenEntity> builder)
    {
        // Configure UserId as the primary key
        builder.HasKey(x => x.TokenId);  // Explicitly telling EF that UserId is the primary key

        builder.Property(x => x.UserId).HasBusinessIdConversion();
        
        builder.Property(x => x.TokenId).HasBusinessIdConversion();
    }
}