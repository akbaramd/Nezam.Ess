using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nezam.ESS.backend.Data.Models;

namespace Nezam.ESS.backend.Data.Configurations;

public class TblPublicUserConfiguration : IEntityTypeConfiguration<TblPublicUser>
{
    public void Configure(EntityTypeBuilder<TblPublicUser> Entity)
    {
        Entity.ToTable("tbl_public_users");

        Entity.Property(e => e.Id).HasColumnName("id");
        Entity.Property(e => e.DNemayandegiCod).HasColumnName("d_nemayandegi_cod");
        Entity.Property(e => e.AccessLevel).HasColumnName("access_level").HasDefaultValue(0);
        Entity.Property(e => e.FirstName).HasMaxLength(50);
        Entity.Property(e => e.VerificationCode).HasMaxLength(6);
        Entity.Property(e => e.LastName).HasMaxLength(50);
        Entity.Property(e => e.Mellicode).HasMaxLength(10);
        Entity.Property(e => e.MobNo)
            .HasMaxLength(12)
            .HasColumnName("Mob_no");
        Entity.Property(e => e.Password).HasMaxLength(50);
        Entity.Property(e => e.SabtDat)
            .HasDefaultValueSql("(getdate())")
            .HasColumnType("datetime")
            .HasColumnName("sabt_dat");
        Entity.Property(e => e.UserName).HasMaxLength(50);
    }
}