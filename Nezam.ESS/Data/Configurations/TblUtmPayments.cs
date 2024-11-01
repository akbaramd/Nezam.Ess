using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nezam.ESS.backend.Data.Models;

namespace Nezam.ESS.backend.Data.Configurations;

public class TblUtmPaymentConfiguration : IEntityTypeConfiguration<TblUtmPayment>
{
    public void Configure(EntityTypeBuilder<TblUtmPayment> entity)
    {
        entity.ToTable("tbl_UTM_payments");

        entity.Property(e => e.Id).HasColumnName("id");
        entity.Property(e => e.Comments)
            .HasMaxLength(50)
            .HasColumnName("comments");
        entity.Property(e => e.FromSite)
            .HasDefaultValue(true)
            .HasColumnName("from_site");
        entity.Property(e => e.KarbarId).HasColumnName("karbar_id");
        entity.Property(e => e.MablagFish).HasColumnName("mablag_fish");
        entity.Property(e => e.SabtDat)
            .HasDefaultValueSql("(getdate())")
            .HasColumnType("datetime")
            .HasColumnName("sabt_dat");
        entity.Property(e => e.SeriFish)
            .HasMaxLength(50)
            .HasColumnName("seri_fish");
        entity.Property(e => e.ShomFish)
            .HasMaxLength(50)
            .HasColumnName("shom_fish");
        entity.Property(e => e.TarikhFish)
            .HasMaxLength(50)
            .HasColumnName("tarikh_fish");
        entity.Property(e => e.Utminfoid).HasColumnName("utminfoid");
    }
}