using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nezam.ESS.backend.Data.Models;

namespace Nezam.ESS.backend.Data.Configurations;

public class TblUtmMaininfoConfiguration : IEntityTypeConfiguration<TblUtmMaininfo>
{
    public void Configure(EntityTypeBuilder<TblUtmMaininfo> entity)
    {
        entity.HasKey(e => e.Utminfoid);

        entity.ToTable("tbl_UTM_maininfo");

        entity.Property(e => e.Address)
            .HasMaxLength(255)
            .HasColumnName("address");
        entity.Property(e => e.Comments).HasColumnName("comments");
        entity.Property(e => e.DNemayandegiCod)
            .HasColumnName("d_nemayandegi_cod");
        entity.Property(e => e.Fname)
            .HasMaxLength(50)
            .HasColumnName("fname");
        entity.Property(e => e.KarbarId).HasColumnName("karbar_id");
        entity.Property(e => e.MantagehId).HasColumnName("mantageh");
        entity.Property(e => e.Mellicode)
            .HasMaxLength(11)
            .HasColumnName("mellicode");
        entity.Property(e => e.Metraj).HasColumnName("metraj");
        entity.Property(e => e.MobNo)
            .HasMaxLength(11)
            .HasColumnName("mob_no");
        entity.Property(e => e.Name)
            .HasMaxLength(50)
            .HasColumnName("name");
        entity.Property(e => e.PelakSabti)
            .HasMaxLength(255)
            .HasColumnName("pelak_sabti");
        entity.Property(e => e.RdGeymatiId).HasColumnName("rd_geymati_id");
        entity.Property(e => e.ReceiveDat)
            .HasMaxLength(10)
            .HasColumnName("receive_dat");
        entity.Property(e => e.SabtDat)
            .HasDefaultValueSql("(getdate())")
            .HasColumnType("datetime")
            .HasColumnName("sabt_dat");
        entity.Property(e => e.State)
            .HasDefaultValue(1)
            .HasColumnName("state");

        entity.HasOne(x => x.Mantageh).WithMany().HasForeignKey(x => x.MantagehId);
        entity.HasOne(x => x.DNemayandegi).WithMany().HasForeignKey(x => x.DNemayandegiCod);
    }
}