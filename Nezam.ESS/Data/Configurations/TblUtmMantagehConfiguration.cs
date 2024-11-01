using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nezam.ESS.backend.Data.Models;

namespace Nezam.ESS.backend.Data.Configurations;

public class TblUtmMantagehConfiguration : IEntityTypeConfiguration<TblUtmMantageh>
{
    public void Configure(EntityTypeBuilder<TblUtmMantageh> entity)
    {
        entity.ToTable("tbl_UTM_mantageh");

        entity.Property(e => e.Id)
            .ValueGeneratedNever()
            .HasColumnName("id");
        entity.Property(e => e.Title)
            .HasMaxLength(50)
            .HasColumnName("title");
    }
}