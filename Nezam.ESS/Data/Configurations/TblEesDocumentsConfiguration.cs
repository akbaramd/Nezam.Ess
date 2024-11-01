using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nezam.ESS.backend.Data.Models;

namespace Nezam.ESS.backend.Data.Configurations;

public class TblEesDocumentsConfiguration : IEntityTypeConfiguration<TblEesDocuments>
{
    public void Configure(EntityTypeBuilder<TblEesDocuments> entity)
    {
        entity.ToTable("tbl_ees_documents");
        entity.HasKey(x => x.Id);
        entity.Property(e => e.Id).HasColumnName("id");
        entity.Property(e => e.UserId).HasColumnName("ozviyat_no");
        entity.Property(e => e.Type).HasColumnName("type");
        entity.Property(e => e.State).HasColumnName("state");
    }
}