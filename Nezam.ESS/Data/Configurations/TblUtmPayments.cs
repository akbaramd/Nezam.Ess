using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nezam.ESS.backend.Data.Models;

namespace Nezam.ESS.backend.Data.Configurations;

public class TblUtmPaymentConfiguration : IEntityTypeConfiguration<TblUtmPayment>
{
    public void Configure(EntityTypeBuilder<TblUtmPayment> Entity)
    {
        Entity.ToTable("tbl_UTM_payments");

        Entity.Property(e => e.Id).HasColumnName("id");
        Entity.Property(e => e.Comments)
            .HasMaxLength(50)
            .HasColumnName("comments");
        Entity.Property(e => e.FromSite)
            .HasDefaultValue(true)
            .HasColumnName("from_site");
        Entity.Property(e => e.KarbarId).HasColumnName("karbar_id");
        Entity.Property(e => e.MablagFish).HasColumnName("mablag_fish");
        Entity.Property(e => e.SabtDat)
            .HasDefaultValueSql("(getdate())")
            .HasColumnType("datetime")
            .HasColumnName("sabt_dat");
        Entity.Property(e => e.SeriFish)
            .HasMaxLength(50)
            .HasColumnName("seri_fish");
        Entity.Property(e => e.ShomFish)
            .HasMaxLength(50)
            .HasColumnName("shom_fish");
        Entity.Property(e => e.TarikhFish)
            .HasMaxLength(50)
            .HasColumnName("tarikh_fish");
        Entity.Property(e => e.Utminfoid).HasColumnName("utminfoid");
    }
}