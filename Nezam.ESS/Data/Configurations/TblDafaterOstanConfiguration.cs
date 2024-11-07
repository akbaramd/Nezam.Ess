using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nezam.ESS.backend.Data.Models;

namespace Nezam.ESS.backend.Data.Configurations;

public class TblDafaterOstanConfiguration : IEntityTypeConfiguration<TblDafaterOstan>
{
    public void Configure(EntityTypeBuilder<TblDafaterOstan> Entity)
    {
        Entity.ToTable("tbl_dafater_ostan");

        Entity.Property(e => e.Id)
            .ValueGeneratedNever()
            .HasColumnName("id");
        Entity.Property(e => e.Address).HasColumnName("address");
        Entity.Property(e => e.CityId).HasColumnName("city_id");
        Entity.Property(e => e.EntekhabatCityCod).HasColumnName("entekhabat_city_cod");
        Entity.Property(e => e.Fax)
            .HasMaxLength(50)
            .HasColumnName("fax");
        Entity.Property(e => e.Gorooh)
            .HasDefaultValue(0)
            .HasColumnName("gorooh");
        Entity.Property(e => e.MarkazMokhTel)
            .HasMaxLength(50)
            .HasColumnName("markaz_mokh_tel");
        Entity.Property(e => e.MerchantIdBargh)
            .HasMaxLength(50)
            .HasColumnName("merchantIdBargh");
        Entity.Property(e => e.MerchantIdGas)
            .HasMaxLength(50)
            .HasColumnName("merchantIdGas");
        Entity.Property(e => e.ParaSrvConstr).HasColumnName("para_srv_constr");
        Entity.Property(e => e.Pass)
            .HasMaxLength(50)
            .HasColumnName("pass");
        Entity.Property(e => e.Phone)
            .HasMaxLength(50)
            .HasColumnName("phone");
        Entity.Property(e => e.PrivateCableNo)
            .HasMaxLength(50)
            .HasColumnName("private_cable_no");
        Entity.Property(e => e.ServerIp)
            .HasMaxLength(255)
            .HasColumnName("server_ip");
        Entity.Property(e => e.SrvConstr).HasColumnName("srv_constr");
        Entity.Property(e => e.SrvConstrInline).HasColumnName("srv_constr_inline");
        Entity.Property(e => e.TejaratBankCod)
            .HasDefaultValue(0)
            .HasColumnName("tejarat_bank_cod");
        Entity.Property(e => e.Title)
            .HasMaxLength(50)
            .HasColumnName("title");
    }
}