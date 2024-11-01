using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nezam.ESS.backend.Data.Models;

namespace Nezam.ESS.backend.Data.Configurations;

public class TblEngineerConfiguration : IEntityTypeConfiguration<TblEngineer>
{
    public void Configure(EntityTypeBuilder<TblEngineer> entity)
    {
        entity.HasKey(e => e.OzviyatNo).HasName("PK_tbl_engineers_ozviyat");

        entity.ToTable("tbl_engineers");

        entity.HasIndex(e => e.CodReshteh, "IX_tbl_engineers_cod_reshteh_3436E");

        entity.HasIndex(e => e.CodReshteh, "IX_tbl_engineers_cod_reshteh_65024");

        entity.HasIndex(e => new { e.CodReshteh, e.DNemayandegiCod, e.StatusCod, e.OzviyatNo },
            "IX_tbl_engineers_cod_reshteh_d_nemayandegi_cod_status_cod_ozviyat_no_9F97C");

        entity.HasIndex(e => new { e.CodReshteh, e.Ncontrol }, "IX_tbl_engineers_cod_reshteh_ncontrol_DD4C5");

        entity.HasIndex(e => new { e.CodReshteh, e.PnezaratCod }, "IX_tbl_engineers_cod_reshteh_pnezarat_cod_B18F3");

        entity.HasIndex(e => new { e.CodReshteh, e.StatusCod }, "IX_tbl_engineers_cod_reshteh_status_cod_C8CFA");

        entity.HasIndex(e => new { e.CodReshteh, e.StatusCod, e.AsliCod, e.Payeh, e.OzviyatNo },
            "IX_tbl_engineers_cod_reshteh_status_cod_asli_cod_payeh_ozviyat_no_BE664");

        entity.HasIndex(
            e => new { e.CodReshteh, e.StatusCod, e.AsliCod, e.Payeh, e.PtarrahiCod, e.PnezaratCod, e.OzviyatNo },
            "IX_tbl_engineers_cod_reshteh_status_cod_asli_cod_payeh_ptarrahi_cod_pnezarat_cod_ozviyat_no_CB359");

        entity.HasIndex(e => e.DNemayandegiCod, "IX_tbl_engineers_d_nemayandegi_cod_8BAEC");

        entity.HasIndex(e => new { e.DNemayandegiCod, e.Fname }, "IX_tbl_engineers_d_nemayandegi_cod_fname_47552");

        entity.HasIndex(
            e => new { e.DNemayandegiCod, e.StatusCod, e.PnezaratCod, e.CodReshteh, e.OzviyatNo, e.Mojri, e.Moshaver },
            "IX_tbl_engineers_d_nemayandegi_cod_status_cod_pnezarat_cod_cod_reshteh_ozviyat_no_mojri_moshaver_4815F");

        entity.HasIndex(e => e.Fname, "IX_tbl_engineers_fname_2411A");

        entity.HasIndex(e => e.MelliCod, "IX_tbl_engineers_melli_cod_9C5CB");

        entity.HasIndex(e => e.ParvanehSodoorDate, "IX_tbl_engineers_parvaneh_sodoor_date_9587B");

        entity.HasIndex(e => e.PnezaratCod, "IX_tbl_engineers_pnezarat_cod_FF692");

        entity.HasIndex(e => e.PtarrahiCod, "IX_tbl_engineers_ptarrahi_cod_65529");

        entity.HasIndex(e => new { e.PtarrahiCod, e.PnezaratCod, e.CodReshteh, e.StatusCod, e.AsliCod, e.OzviyatNo },
            "IX_tbl_engineers_ptarrahi_cod_pnezarat_cod_cod_reshteh_status_cod_asli_cod_ozviyat_no_DF10D");

        entity.HasIndex(
            e => new { e.PtarrahiCod, e.PnezaratCod, e.CodReshteh, e.StatusCod, e.AsliCod, e.OzviyatNo, e.PayehEjra },
            "IX_tbl_engineers_ptarrahi_cod_pnezarat_cod_cod_reshteh_status_cod_asli_cod_ozviyat_no_payeh_ejra_94A89");

        entity.HasIndex(
            e => new
            {
                e.PtarrahiCod, e.PnezaratCod, e.CodReshteh, e.StatusCod, e.AsliCod, e.OzviyatNo, e.Payeh, e.PayehEjra
            },
            "IX_tbl_engineers_ptarrahi_cod_pnezarat_cod_cod_reshteh_status_cod_asli_cod_ozviyat_no_payeh_payeh_ejra_44AB0");

        entity.HasIndex(
            e => new { e.PtarrahiCod, e.PnezaratCod, e.CodReshteh, e.StatusCod, e.AsliCod, e.PayehEjra, e.OzviyatNo },
            "IX_tbl_engineers_ptarrahi_cod_pnezarat_cod_cod_reshteh_status_cod_asli_cod_payeh_ejra_ozviyat_no_EB618");

        entity.HasIndex(
            e => new { e.PtarrahiCod, e.PnezaratCod, e.CodReshteh, e.StatusCod, e.AsliCod, e.Payeh, e.OzviyatNo },
            "IX_tbl_engineers_ptarrahi_cod_pnezarat_cod_cod_reshteh_status_cod_asli_cod_payeh_ozviyat_no_324B9");

        entity.HasIndex(e => new { e.Sex, e.StatusCod, e.OzviyatNo },
            "IX_tbl_engineers_sex_status_cod_ozviyat_no_6111E");

        entity.HasIndex(e => new { e.Sex, e.StatusCod, e.OzviyatNo, e.Payeh },
            "IX_tbl_engineers_sex_status_cod_ozviyat_no_payeh_DAD82");

        entity.HasIndex(e => new { e.StatusCod, e.CodReshteh, e.OzviyatNo, e.DNemayandegiCod },
            "IX_tbl_engineers_status_cod_cod_reshteh_ozviyat_no_d_nemayandegi_cod_D1CAE");

        entity.HasIndex(e => new { e.StatusCod, e.CodReshteh, e.OzviyatNo, e.Payeh },
            "IX_tbl_engineers_status_cod_cod_reshteh_ozviyat_no_payeh_7E5D4");

        entity.HasIndex(e => new { e.StatusCod, e.Fname, e.OzviyatNo, e.DNemayandegiCod },
            "IX_tbl_engineers_status_cod_fname_ozviyat_no_d_nemayandegi_cod_CCD84");

        entity.HasIndex(e => new { e.StatusCod, e.OzviyatNo }, "IX_tbl_engineers_status_cod_ozviyat_no_14848");

        entity.HasIndex(e => new { e.StatusCod, e.OzviyatNo }, "IX_tbl_engineers_status_cod_ozviyat_no_F3AB2");

        entity.HasIndex(e => new { e.StatusCod, e.OzviyatNo, e.Payeh },
            "IX_tbl_engineers_status_cod_ozviyat_no_payeh_B8106");

        entity.HasIndex(e => new { e.Id, e.StatusCod, e.OzviyatNo }, "idx1");

        entity.Property(e => e.OzviyatNo)
            .ValueGeneratedNever()
            .HasColumnName("ozviyat_no");
        entity.Property(e => e.Address)
            .HasMaxLength(250)
            .HasColumnName("address");
        entity.Property(e => e.AddressState)
            .HasDefaultValue(true)
            .HasColumnName("address_state");
        entity.Property(e => e.AsliCod).HasColumnName("asli_cod");
        entity.Property(e => e.BornCity).HasColumnName("born_city");
        entity.Property(e => e.BornDate)
            .HasMaxLength(50)
            .HasColumnName("born_date");
        entity.Property(e => e.CodPostiJob)
            .HasMaxLength(12)
            .HasColumnName("cod_posti_job");
        entity.Property(e => e.CodPostiManzel)
            .HasMaxLength(12)
            .HasColumnName("cod_posti_manzel");
        entity.Property(e => e.CodReshteh).HasColumnName("cod_reshteh");
        entity.Property(e => e.Comments)
            .HasMaxLength(250)
            .HasColumnName("comments");
        entity.Property(e => e.Control).HasColumnName("control");
        entity.Property(e => e.CoronaTamdidDat)
            .HasMaxLength(50)
            .HasColumnName("corona_tamdid_dat");
        entity.Property(e => e.DNemayandegiCod).HasColumnName("d_nemayandegi_cod");
        entity.Property(e => e.DaftarNo)
            .HasMaxLength(50)
            .HasColumnName("daftar_no");
        entity.Property(e => e.EMail)
            .HasMaxLength(100)
            .HasColumnName("e_mail");
        entity.Property(e => e.EndDate)
            .HasMaxLength(50)
            .HasColumnName("end_date");
        entity.Property(e => e.EndDateTemp)
            .HasMaxLength(50)
            .HasColumnName("end_date_temp");
        entity.Property(e => e.EndMetrajNezarat)
            .HasDefaultValue(0L)
            .HasColumnName("end_metraj_nezarat");
        entity.Property(e => e.EndMetrajTarrahi)
            .HasDefaultValue(0L)
            .HasColumnName("end_metraj_tarrahi");
        entity.Property(e => e.EndTedadNezarat)
            .HasDefaultValue(0)
            .HasColumnName("end_tedad_nezarat");
        entity.Property(e => e.EndTedadTarrahi)
            .HasDefaultValue(0)
            .HasColumnName("end_tedad_tarrahi");
        entity.Property(e => e.EnglishName)
            .HasMaxLength(50)
            .HasColumnName("english_name");
        entity.Property(e => e.EntegalCodOstan).HasColumnName("entegal_cod_ostan");
        entity.Property(e => e.EstelamDat)
            .HasMaxLength(50)
            .HasColumnName("estelam_dat");
        entity.Property(e => e.EstelamNumber)
            .HasMaxLength(50)
            .HasColumnName("estelam_number");
        entity.Property(e => e.EstelamResult)
            .HasDefaultValue(false)
            .HasColumnName("estelam_result");
        entity.Property(e => e.FlCod).HasColumnName("fl_cod");
        entity.Property(e => e.Fname)
            .HasMaxLength(50)
            .HasColumnName("fname");
        entity.Property(e => e.FullName)
            .HasMaxLength(50)
            .HasColumnName("full_name");
        entity.Property(e => e.Id)
            .ValueGeneratedOnAdd()
            .HasColumnName("id");
        entity.Property(e => e.Idno)
            .HasMaxLength(50)
            .HasColumnName("idno");
        entity.Property(e => e.Img)
            .HasColumnType("image")
            .HasColumnName("img");
        entity.Property(e => e.JobAddress)
            .HasMaxLength(250)
            .HasColumnName("job_address");
        entity.Property(e => e.JobAddressState)
            .HasDefaultValue(false)
            .HasColumnName("job_address_state");
        entity.Property(e => e.KarbarId).HasColumnName("karbar_id");
        entity.Property(e => e.Maghta).HasColumnName("maghta");
        entity.Property(e => e.Mah).HasColumnName("mah");
        entity.Property(e => e.Main)
            .HasMaxLength(50)
            .HasColumnName("main");
        entity.Property(e => e.MelliCod)
            .HasMaxLength(15)
            .HasColumnName("melli_cod");
        entity.Property(e => e.Military)
            .HasDefaultValue(0)
            .HasColumnName("military");
        entity.Property(e => e.MobNo)
            .HasMaxLength(50)
            .HasDefaultValueSql("((0))")
            .HasColumnName("mob_no");
        entity.Property(e => e.ModifiedDat)
            .HasDefaultValueSql("(getdate())")
            .HasColumnType("datetime")
            .HasColumnName("modified_dat");
        entity.Property(e => e.Mojri).HasColumnName("mojri");
        entity.Property(e => e.Moshaver)
            .HasDefaultValue(false)
            .HasColumnName("moshaver");
        entity.Property(e => e.Name)
            .HasMaxLength(50)
            .HasColumnName("name");
        entity.Property(e => e.NazerJaygozin)
            .HasMaxLength(150)
            .HasColumnName("nazer_jaygozin");
        entity.Property(e => e.NazerMoghim)
            .HasDefaultValue(false)
            .HasColumnName("nazer_moghim");
        entity.Property(e => e.NazerMoghimComment)
            .HasMaxLength(255)
            .HasColumnName("nazer_moghim_comment");
        entity.Property(e => e.Ncontrol).HasColumnName("ncontrol");
        entity.Property(e => e.OldControlMap).HasColumnName("old_control_map");
        entity.Property(e => e.OldControlNezarat).HasColumnName("old_control_nezarat");
        entity.Property(e => e.OstanCod).HasColumnName("ostan_cod");
        entity.Property(e => e.OzvDate)
            .HasMaxLength(50)
            .HasColumnName("ozv_date");
        entity.Property(e => e.OzvExpDate)
            .HasMaxLength(50)
            .HasColumnName("ozv_exp_date");
        entity.Property(e => e.OzviyatMark)
            .HasMaxLength(50)
            .HasColumnName("ozviyat_mark");
        entity.Property(e => e.ParvanehMark)
            .HasMaxLength(50)
            .HasColumnName("parvaneh_mark");
        entity.Property(e => e.ParvanehNo).HasColumnName("parvaneh_no");
        entity.Property(e => e.ParvanehOstan)
            .HasDefaultValue(14)
            .HasColumnName("parvaneh_ostan");
        entity.Property(e => e.ParvanehSodoorDate)
            .HasMaxLength(50)
            .HasColumnName("parvaneh_sodoor_date");
        entity.Property(e => e.ParvanehStatus)
            .HasDefaultValue(0)
            .HasColumnName("parvaneh_status");
        entity.Property(e => e.Password)
            .HasMaxLength(50)
            .HasColumnName("password");
        entity.Property(e => e.Payeh).HasColumnName("payeh");
        entity.Property(e => e.PayehEjra)
            .HasDefaultValue(0)
            .HasColumnName("payeh_ejra");
        entity.Property(e => e.PayehType)
            .HasDefaultValue(0)
            .HasColumnName("payeh_type");
        entity.Property(e => e.Pname)
            .HasMaxLength(50)
            .HasColumnName("pname");
        entity.Property(e => e.PnezaratCod).HasColumnName("pnezarat_cod");
        entity.Property(e => e.PtarrahiCod).HasColumnName("ptarrahi_cod");
        entity.Property(e => e.RelOzviyatNo)
            .HasMaxLength(15)
            .HasColumnName("rel_ozviyat_no");
        entity.Property(e => e.RequestNezarat)
            .HasDefaultValue(false)
            .HasColumnName("request_nezarat");
        entity.Property(e => e.ReshtehName)
            .HasMaxLength(50)
            .HasColumnName("reshteh_name");
        entity.Property(e => e.Rooz).HasColumnName("rooz");
        entity.Property(e => e.SabtDate)
            .HasMaxLength(50)
            .HasColumnName("sabt_date");
        entity.Property(e => e.Sal).HasColumnName("sal");
        entity.Property(e => e.Sex).HasColumnName("sex");
        entity.Property(e => e.SignAllow)
            .HasDefaultValue(false)
            .HasColumnName("sign_allow");
        entity.Property(e => e.StatusCod).HasColumnName("status_cod");
        entity.Property(e => e.TaaholState)
            .HasDefaultValue(0)
            .HasColumnName("taahol_state");
        entity.Property(e => e.TafkikActive)
            .HasDefaultValue(0)
            .HasColumnName("tafkik_active");
        entity.Property(e => e.TafkikCert)
            .HasDefaultValue(false)
            .HasColumnName("tafkik_cert");
        entity.Property(e => e.TamdidDate)
            .HasMaxLength(50)
            .HasColumnName("tamdid_date");
        entity.Property(e => e.Tasviye).HasColumnName("tasviye");
        entity.Property(e => e.TelJob)
            .HasMaxLength(50)
            .HasColumnName("tel_job");
        entity.Property(e => e.TelNo)
            .HasMaxLength(50)
            .HasColumnName("tel_no");
        entity.Property(e => e.University)
            .HasMaxLength(100)
            .HasColumnName("university");
        entity.Property(e => e.UniversityCod)
            .HasDefaultValue(0)
            .HasColumnName("university_cod");
        entity.Property(e => e.UniversityDate)
            .HasMaxLength(50)
            .HasColumnName("university_date");
        entity.Property(e => e.Website)
            .HasMaxLength(50)
            .HasColumnName("website");
    }
}