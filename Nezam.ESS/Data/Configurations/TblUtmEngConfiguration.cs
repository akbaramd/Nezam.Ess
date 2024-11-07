using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nezam.ESS.backend.Data.Models;

namespace Nezam.ESS.backend.Data.Configurations;

public class TblUtmEngConfiguration : IEntityTypeConfiguration<TblUtmEng>
{
    public void Configure(EntityTypeBuilder<TblUtmEng> Entity)
    {
        Entity.ToTable("tbl_UTM_engs");

        Entity.Property(e => e.Id).HasColumnName("id");
        Entity.Property(e => e.ActiveType)
            .HasDefaultValue(1)
            .HasColumnName("active_type");
        Entity.Property(e => e.Comments)
            .HasMaxLength(255)
            .HasColumnName("comments");
        Entity.Property(e => e.EngCod).HasColumnName("eng_cod");
        Entity.Property(e => e.KarbarId).HasColumnName("karbar_id");
        Entity.Property(e => e.OzviyatNo).HasColumnName("ozviyat_no");
        Entity.Property(e => e.SabtDat)
            .HasDefaultValueSql("(getdate())")
            .HasColumnType("datetime")
            .HasColumnName("sabt_dat");

        Entity
            .HasOne(d => d.OzviyatNoNavigation)
            .WithMany(p => p.TblUtmEngs)
            .HasForeignKey(d => d.OzviyatNo)
            .HasPrincipalKey(d => d.OzviyatNo);
    }
}