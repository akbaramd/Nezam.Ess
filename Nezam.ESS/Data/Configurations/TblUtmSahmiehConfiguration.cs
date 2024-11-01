using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nezam.ESS.backend.Data.Models;

namespace Nezam.ESS.backend.Data.Configurations;

public class TblUtmSahmiehConfiguration : IEntityTypeConfiguration<TblUtmSahmieh>
{
    public void Configure(EntityTypeBuilder<TblUtmSahmieh> entity)
    {
        entity.ToTable("tbl_UTM_sahmieh");

        entity.Property(e => e.Id).HasColumnName("id");
        entity.Property(e => e.Comments)
            .HasMaxLength(50)
            .HasColumnName("comments");
        entity.Property(e => e.EngCod).HasColumnName("eng_cod");
        entity.Property(e => e.KarbarId).HasColumnName("karbar_id");
        entity.Property(e => e.SabtDat)
            .HasDefaultValueSql("(getdate())")
            .HasColumnType("datetime")
            .HasColumnName("sabt_dat");
        entity.Property(e => e.State)
            .HasDefaultValue(1)
            .HasColumnName("state");
        entity.Property(e => e.Utminfoid).HasColumnName("utminfoid");


        entity.HasOne(x => x.Eng)
            .WithMany(x => x.TblUtmSahmiehs)
            .HasForeignKey(x => x.EngCod)
            .HasPrincipalKey(x => x.EngCod);
    }
}