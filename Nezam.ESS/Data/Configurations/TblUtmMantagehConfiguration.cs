using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nezam.ESS.backend.Data.Models;

namespace Nezam.ESS.backend.Data.Configurations;

public class TblUtmMantagehConfiguration : IEntityTypeConfiguration<TblUtmMantageh>
{
    public void Configure(EntityTypeBuilder<TblUtmMantageh> Entity)
    {
        Entity.ToTable("tbl_UTM_mantageh");

        Entity.Property(e => e.Id)
            .ValueGeneratedNever()
            .HasColumnName("id");
        Entity.Property(e => e.Title)
            .HasMaxLength(50)
            .HasColumnName("title");
    }
}