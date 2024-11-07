using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nezam.ESS.backend.Data.Models;

namespace Nezam.ESS.backend.Data.Configurations;

public class TblUtmMaininfoConfiguration : IEntityTypeConfiguration<TblUtmMaininfo>
{
    public void Configure(EntityTypeBuilder<TblUtmMaininfo> Entity)
    {
        Entity.HasKey(e => e.Utminfoid);

        Entity.ToTable("tbl_UTM_maininfo");

        Entity.Property(e => e.Address)
            .HasMaxLength(255)
            .HasColumnName("address");
        Entity.Property(e => e.Comments).HasColumnName("comments");
        Entity.Property(e => e.DNemayandegiCod)
            .HasColumnName("d_nemayandegi_cod");
        Entity.Property(e => e.Fname)
            .HasMaxLength(50)
            .HasColumnName("fname");
        Entity.Property(e => e.KarbarId).HasColumnName("karbar_id");
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
        Entity.Property(e => e.RdGeymatiId).HasColumnName("rd_geymati_id");
        Entity.Property(e => e.ReceiveDat)
            .HasMaxLength(10)
            .HasColumnName("receive_dat");
        Entity.Property(e => e.SabtDat)
            .HasDefaultValueSql("(getdate())")
            .HasColumnType("datetime")
            .HasColumnName("sabt_dat");
        Entity.Property(e => e.State)
            .HasDefaultValue(1)
            .HasColumnName("state");

        Entity.HasOne(x => x.Mantageh).WithMany().HasForeignKey(x => x.MantagehId);
        Entity.HasOne(x => x.DNemayandegi).WithMany().HasForeignKey(x => x.DNemayandegiCod);
    }
}