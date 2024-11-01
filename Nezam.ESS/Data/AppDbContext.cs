using Microsoft.EntityFrameworkCore;
using Nezam.ESS.backend.Data.Configurations;
using Nezam.ESS.backend.Data.Models;

namespace Nezam.ESS.backend.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public virtual required DbSet<TblPublicUser> TblPublicUsers { get; set; }
    public virtual required DbSet<TblDafaterOstan> TblDafaterOstans { get; set; }
    public virtual required DbSet<TblUtmMaininfo> TblUtmMaininfos { get; set; }
    public virtual required DbSet<TblUtmMerchants> TblUtmMerchants { get; set; }
    public virtual required DbSet<TblUtmPayment> TblUtmPayments { get; set; }
    public virtual required DbSet<TblUtmMaininfoTemp> TblUtmMaininfoTemps { get; set; }
    public virtual required DbSet<TblUtmMantageh> TblUtmMantagehs { get; set; }
    public virtual required DbSet<TblUtmTaarefe> TblUtmTaarefes { get; set; }
    public virtual required DbSet<TblUtmEng> TblUtmEngs { get; set; }
    public virtual required DbSet<TblUtmSahmieh> TblUtmSahmiehs { get; set; }
    public virtual required DbSet<TblEesDocuments> TblEesDocuments { get; set; }
    public virtual required DbSet<TblEngineer> TblEngineers { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new TblPublicUserConfiguration());
        modelBuilder.ApplyConfiguration(new TblUtmMerchantsConfiguration());
        modelBuilder.ApplyConfiguration(new TblDafaterOstanConfiguration());
        modelBuilder.ApplyConfiguration(new TblUtmMantagehConfiguration());
        modelBuilder.ApplyConfiguration(new TblUtmMaininfoConfiguration());
        modelBuilder.ApplyConfiguration(new TblUtmPaymentConfiguration());
        modelBuilder.ApplyConfiguration(new TblUtmMaininfoTempConfiguration());
        modelBuilder.ApplyConfiguration(new TblUtmTaarefeConfiguration());
        modelBuilder.ApplyConfiguration(new TblUtmSahmiehConfiguration());
        modelBuilder.ApplyConfiguration(new TblUtmEngConfiguration());
        modelBuilder.ApplyConfiguration(new TblEngineerConfiguration());
        modelBuilder.ApplyConfiguration(new TblEesDocumentsConfiguration());
    }
}