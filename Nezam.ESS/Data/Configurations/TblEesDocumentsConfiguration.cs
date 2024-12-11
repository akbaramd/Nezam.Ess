using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nezam.ESS.backend.Data.Models;

namespace Nezam.ESS.backend.Data.Configurations;

public class TblEesDocumentsConfiguration : IEntityTypeConfiguration<TblEesDocuments>
{
    public void Configure(EntityTypeBuilder<TblEesDocuments> Entity)
    {
        Entity.ToTable("tbl_ees_documents");
        Entity.HasKey(x => x.Id);
        Entity.Property(e => e.Id).HasColumnName("id");
        Entity.Property(e => e.UserId).HasColumnName("ozviyat_no");
        Entity.Property(e => e.Type).HasColumnName("type");
        Entity.Property(e => e.State).HasColumnName("state");
    }
}