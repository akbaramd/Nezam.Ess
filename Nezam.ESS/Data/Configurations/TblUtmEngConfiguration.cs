using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nezam.ESS.backend.Data.Models;

namespace Nezam.ESS.backend.Data.Configurations;

public class TblUtmEngConfiguration : IEntityTypeConfiguration<TblUtmEng>
{
    public void Configure(EntityTypeBuilder<TblUtmEng> entity)
    {
        entity.ToTable("tbl_UTM_engs");

        entity.Property(e => e.Id).HasColumnName("id");
        entity.Property(e => e.ActiveType)
            .HasDefaultValue(1)
            .HasColumnName("active_type");
        entity.Property(e => e.Comments)
            .HasMaxLength(255)
            .HasColumnName("comments");
        entity.Property(e => e.EngCod).HasColumnName("eng_cod");
        entity.Property(e => e.KarbarId).HasColumnName("karbar_id");
        entity.Property(e => e.OzviyatNo).HasColumnName("ozviyat_no");
        entity.Property(e => e.SabtDat)
            .HasDefaultValueSql("(getdate())")
            .HasColumnType("datetime")
            .HasColumnName("sabt_dat");

        entity
            .HasOne(d => d.OzviyatNoNavigation)
            .WithMany(p => p.TblUtmEngs)
            .HasForeignKey(d => d.OzviyatNo)
            .HasPrincipalKey(d => d.OzviyatNo);
    }
}