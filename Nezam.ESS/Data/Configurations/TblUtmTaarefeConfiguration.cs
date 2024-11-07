using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nezam.ESS.backend.Data.Models;

namespace Nezam.ESS.backend.Data.Configurations;

public class TblUtmTaarefeConfiguration : IEntityTypeConfiguration<TblUtmTaarefe>
{
    public void Configure(EntityTypeBuilder<TblUtmTaarefe> Entity)
    {
        Entity.ToTable("tbl_UTM_taarefe");

        Entity.Property(e => e.Id).HasColumnName("id");
        Entity.Property(e => e.Comments)
            .HasMaxLength(50)
            .HasColumnName("comments");
        Entity.Property(e => e.DNemayandegiCod).HasColumnName("d_nemayandegi_cod");
        Entity.Property(e => e.Mablag).HasColumnName("mablag");
        Entity.Property(e => e.MaxMetraj).HasColumnName("max_metraj");
        Entity.Property(e => e.MinMetraj).HasColumnName("min_metraj");
        Entity.Property(e => e.RdGeymati).HasColumnName("rd_geymati");
        Entity.Property(e => e.Shahri)
            .HasDefaultValue(1)
            .HasColumnName("shahri");
    }
}