using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nezam.ESS.backend.Data.Models;

namespace Nezam.ESS.backend.Data.Configurations;

public class TblPublicUserConfiguration : IEntityTypeConfiguration<TblPublicUser>
{
    public void Configure(EntityTypeBuilder<TblPublicUser> entity)
    {
        entity.ToTable("tbl_public_users");

        entity.Property(e => e.Id).HasColumnName("id");
        entity.Property(e => e.DNemayandegiCod).HasColumnName("d_nemayandegi_cod");
        entity.Property(e => e.AccessLevel).HasColumnName("access_level").HasDefaultValue(0);
        entity.Property(e => e.FirstName).HasMaxLength(50);
        entity.Property(e => e.VerificationCode).HasMaxLength(6);
        entity.Property(e => e.LastName).HasMaxLength(50);
        entity.Property(e => e.Mellicode).HasMaxLength(10);
        entity.Property(e => e.MobNo)
            .HasMaxLength(12)
            .HasColumnName("Mob_no");
        entity.Property(e => e.Password).HasMaxLength(50);
        entity.Property(e => e.SabtDat)
            .HasDefaultValueSql("(getdate())")
            .HasColumnType("datetime")
            .HasColumnName("sabt_dat");
        entity.Property(e => e.UserName).HasMaxLength(50);
    }
}