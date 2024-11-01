using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nezam.ESS.backend.Data.Models;

namespace Nezam.ESS.backend.Data.Configurations;

public class TblUtmTaarefeConfiguration : IEntityTypeConfiguration<TblUtmTaarefe>
{
    public void Configure(EntityTypeBuilder<TblUtmTaarefe> entity)
    {
        entity.ToTable("tbl_UTM_taarefe");

        entity.Property(e => e.Id).HasColumnName("id");
        entity.Property(e => e.Comments)
            .HasMaxLength(50)
            .HasColumnName("comments");
        entity.Property(e => e.DNemayandegiCod).HasColumnName("d_nemayandegi_cod");
        entity.Property(e => e.Mablag).HasColumnName("mablag");
        entity.Property(e => e.MaxMetraj).HasColumnName("max_metraj");
        entity.Property(e => e.MinMetraj).HasColumnName("min_metraj");
        entity.Property(e => e.RdGeymati).HasColumnName("rd_geymati");
        entity.Property(e => e.Shahri)
            .HasDefaultValue(1)
            .HasColumnName("shahri");
    }
}