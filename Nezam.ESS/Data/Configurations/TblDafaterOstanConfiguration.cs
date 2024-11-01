using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nezam.ESS.backend.Data.Models;

namespace Nezam.ESS.backend.Data.Configurations;

public class TblDafaterOstanConfiguration : IEntityTypeConfiguration<TblDafaterOstan>
{
    public void Configure(EntityTypeBuilder<TblDafaterOstan> entity)
    {
        entity.ToTable("tbl_dafater_ostan");

        entity.Property(e => e.Id)
            .ValueGeneratedNever()
            .HasColumnName("id");
        entity.Property(e => e.Address).HasColumnName("address");
        entity.Property(e => e.CityId).HasColumnName("city_id");
        entity.Property(e => e.EntekhabatCityCod).HasColumnName("entekhabat_city_cod");
        entity.Property(e => e.Fax)
            .HasMaxLength(50)
            .HasColumnName("fax");
        entity.Property(e => e.Gorooh)
            .HasDefaultValue(0)
            .HasColumnName("gorooh");
        entity.Property(e => e.MarkazMokhTel)
            .HasMaxLength(50)
            .HasColumnName("markaz_mokh_tel");
        entity.Property(e => e.MerchantIdBargh)
            .HasMaxLength(50)
            .HasColumnName("merchantIdBargh");
        entity.Property(e => e.MerchantIdGas)
            .HasMaxLength(50)
            .HasColumnName("merchantIdGas");
        entity.Property(e => e.ParaSrvConstr).HasColumnName("para_srv_constr");
        entity.Property(e => e.Pass)
            .HasMaxLength(50)
            .HasColumnName("pass");
        entity.Property(e => e.Phone)
            .HasMaxLength(50)
            .HasColumnName("phone");
        entity.Property(e => e.PrivateCableNo)
            .HasMaxLength(50)
            .HasColumnName("private_cable_no");
        entity.Property(e => e.ServerIp)
            .HasMaxLength(255)
            .HasColumnName("server_ip");
        entity.Property(e => e.SrvConstr).HasColumnName("srv_constr");
        entity.Property(e => e.SrvConstrInline).HasColumnName("srv_constr_inline");
        entity.Property(e => e.TejaratBankCod)
            .HasDefaultValue(0)
            .HasColumnName("tejarat_bank_cod");
        entity.Property(e => e.Title)
            .HasMaxLength(50)
            .HasColumnName("title");
    }
}