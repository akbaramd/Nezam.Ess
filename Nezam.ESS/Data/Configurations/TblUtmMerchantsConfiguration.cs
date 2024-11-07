using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nezam.ESS.backend.Data.Models;

namespace Nezam.ESS.backend.Data.Configurations;

public class TblUtmMerchantsConfiguration : IEntityTypeConfiguration<TblUtmMerchants>
{
    public void Configure(EntityTypeBuilder<TblUtmMerchants> Entity)
    {
        Entity.ToTable("tbl_UTM_merchants");

        Entity.Property(e => e.id).HasColumnName("id");
    }
}