using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nezam.ESS.backend.Data.Models;

namespace Nezam.ESS.backend.Data.Configurations;

public class TblUtmMaininfoTempConfiguration : IEntityTypeConfiguration<TblUtmMaininfoTemp>
{
    public void Configure(EntityTypeBuilder<TblUtmMaininfoTemp> Entity)
    {
        Entity.HasKey(e => e.IdTemp);

        Entity.ToTable("tbl_UTM_maininfo_temp");

        Entity.Property(e => e.IdTemp).HasColumnName("id_temp");
        Entity.Property(e => e.TrackingNumber).HasColumnName("tracking_number");
        Entity.Property(e => e.Address)
            .HasMaxLength(255)
            .HasColumnName("address");
        Entity.Property(e => e.DNemayandegiCod)
            .HasColumnName("d_nemayandegi_cod");
        Entity.Property(e => e.Fname)
            .HasMaxLength(50)
            .HasColumnName("fname");
        Entity.Property(e => e.MantagehId).HasColumnName("mantageh");
        Entity.Property(e => e.Mellicode)
            .HasMaxLength(11)
            .HasColumnName("mellicode");
        Entity.Property(e => e.Metraj).HasColumnName("metraj");
        Entity.Property(e => e.MobNo)
            .HasMaxLength(11)
            .HasColumnName("mob_no");
        Entity.Property(e => e.Name)
            .HasMaxLength(50)
            .HasColumnName("name");
        Entity.Property(e => e.PelakSabti)
            .HasMaxLength(255)
            .HasColumnName("pelak_sabti");
        Entity.Property(e => e.RegBonUserId).HasColumnName("reg_BonUserId");
        Entity.Property(e => e.State).HasColumnName("state");
        Entity.Property(e => e.SabtDat)
            .HasDefaultValueSql("(getdate())")
            .HasColumnType("datetime")
            .HasColumnName("sabt_dat");

        Entity.Property(e => e.MainInfoId)
            .ValueGeneratedNever()
            .HasColumnName("main_info_id");

        Entity.HasOne(x => x.Mantageh).WithMany().HasForeignKey(x => x.MantagehId);
        Entity.HasOne(x => x.DNemayandegi).WithMany().HasForeignKey(x => x.DNemayandegiCod);
    }
}