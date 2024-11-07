using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nezam.ESS.backend.Data.Models;

namespace Nezam.ESS.backend.Data.Configurations;

public class TblUtmSahmiehConfiguration : IEntityTypeConfiguration<TblUtmSahmieh>
{
    public void Configure(EntityTypeBuilder<TblUtmSahmieh> Entity)
    {
        Entity.ToTable("tbl_UTM_sahmieh");

        Entity.Property(e => e.Id).HasColumnName("id");
        Entity.Property(e => e.Comments)
            .HasMaxLength(50)
            .HasColumnName("comments");
        Entity.Property(e => e.EngCod).HasColumnName("eng_cod");
        Entity.Property(e => e.KarbarId).HasColumnName("karbar_id");
        Entity.Property(e => e.SabtDat)
            .HasDefaultValueSql("(getdate())")
            .HasColumnType("datetime")
            .HasColumnName("sabt_dat");
        Entity.Property(e => e.State)
            .HasDefaultValue(1)
            .HasColumnName("state");
        Entity.Property(e => e.Utminfoid).HasColumnName("utminfoid");


        Entity.HasOne(x => x.Eng)
            .WithMany(x => x.TblUtmSahmiehs)
            .HasForeignKey(x => x.EngCod)
            .HasPrincipalKey(x => x.EngCod);
    }
}