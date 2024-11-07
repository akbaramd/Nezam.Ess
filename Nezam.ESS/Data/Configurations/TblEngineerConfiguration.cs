using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nezam.ESS.backend.Data.Models;

namespace Nezam.ESS.backend.Data.Configurations;

public class TblEngineerConfiguration : IEntityTypeConfiguration<TblEngineer>
{
    public void Configure(EntityTypeBuilder<TblEngineer> Entity)
    {
        Entity.HasKey(e => e.OzviyatNo).HasName("PK_tbl_engineers_ozviyat");

        Entity.ToTable("tbl_engineers");

        Entity.HasIndex(e => e.CodReshteh, "IX_tbl_engineers_cod_reshteh_3436E");

        Entity.HasIndex(e => e.CodReshteh, "IX_tbl_engineers_cod_reshteh_65024");

        Entity.HasIndex(e => new { e.CodReshteh, e.DNemayandegiCod, e.StatusCod, e.OzviyatNo },
            "IX_tbl_engineers_cod_reshteh_d_nemayandegi_cod_status_cod_ozviyat_no_9F97C");

        Entity.HasIndex(e => new { e.CodReshteh, e.Ncontrol }, "IX_tbl_engineers_cod_reshteh_ncontrol_DD4C5");

        Entity.HasIndex(e => new { e.CodReshteh, e.PnezaratCod }, "IX_tbl_engineers_cod_reshteh_pnezarat_cod_B18F3");

        Entity.HasIndex(e => new { e.CodReshteh, e.StatusCod }, "IX_tbl_engineers_cod_reshteh_status_cod_C8CFA");

        Entity.HasIndex(e => new { e.CodReshteh, e.StatusCod, e.AsliCod, e.Payeh, e.OzviyatNo },
            "IX_tbl_engineers_cod_reshteh_status_cod_asli_cod_payeh_ozviyat_no_BE664");

        Entity.HasIndex(
            e => new { e.CodReshteh, e.StatusCod, e.AsliCod, e.Payeh, e.PtarrahiCod, e.PnezaratCod, e.OzviyatNo },
            "IX_tbl_engineers_cod_reshteh_status_cod_asli_cod_payeh_ptarrahi_cod_pnezarat_cod_ozviyat_no_CB359");

        Entity.HasIndex(e => e.DNemayandegiCod, "IX_tbl_engineers_d_nemayandegi_cod_8BAEC");

        Entity.HasIndex(e => new { e.DNemayandegiCod, e.Fname }, "IX_tbl_engineers_d_nemayandegi_cod_fname_47552");

        Entity.HasIndex(
            e => new { e.DNemayandegiCod, e.StatusCod, e.PnezaratCod, e.CodReshteh, e.OzviyatNo, e.Mojri, e.Moshaver },
            "IX_tbl_engineers_d_nemayandegi_cod_status_cod_pnezarat_cod_cod_reshteh_ozviyat_no_mojri_moshaver_4815F");

        Entity.HasIndex(e => e.Fname, "IX_tbl_engineers_fname_2411A");

        Entity.HasIndex(e => e.MelliCod, "IX_tbl_engineers_melli_cod_9C5CB");

        Entity.HasIndex(e => e.ParvanehSodoorDate, "IX_tbl_engineers_parvaneh_sodoor_date_9587B");

        Entity.HasIndex(e => e.PnezaratCod, "IX_tbl_engineers_pnezarat_cod_FF692");

        Entity.HasIndex(e => e.PtarrahiCod, "IX_tbl_engineers_ptarrahi_cod_65529");

        Entity.HasIndex(e => new { e.PtarrahiCod, e.PnezaratCod, e.CodReshteh, e.StatusCod, e.AsliCod, e.OzviyatNo },
            "IX_tbl_engineers_ptarrahi_cod_pnezarat_cod_cod_reshteh_status_cod_asli_cod_ozviyat_no_DF10D");

        Entity.HasIndex(
            e => new { e.PtarrahiCod, e.PnezaratCod, e.CodReshteh, e.StatusCod, e.AsliCod, e.OzviyatNo, e.PayehEjra },
            "IX_tbl_engineers_ptarrahi_cod_pnezarat_cod_cod_reshteh_status_cod_asli_cod_ozviyat_no_payeh_ejra_94A89");

        Entity.HasIndex(
            e => new
            {
                e.PtarrahiCod, e.PnezaratCod, e.CodReshteh, e.StatusCod, e.AsliCod, e.OzviyatNo, e.Payeh, e.PayehEjra
            },
            "IX_tbl_engineers_ptarrahi_cod_pnezarat_cod_cod_reshteh_status_cod_asli_cod_ozviyat_no_payeh_payeh_ejra_44AB0");

        Entity.HasIndex(
            e => new { e.PtarrahiCod, e.PnezaratCod, e.CodReshteh, e.StatusCod, e.AsliCod, e.PayehEjra, e.OzviyatNo },
            "IX_tbl_engineers_ptarrahi_cod_pnezarat_cod_cod_reshteh_status_cod_asli_cod_payeh_ejra_ozviyat_no_EB618");

        Entity.HasIndex(
            e => new { e.PtarrahiCod, e.PnezaratCod, e.CodReshteh, e.StatusCod, e.AsliCod, e.Payeh, e.OzviyatNo },
            "IX_tbl_engineers_ptarrahi_cod_pnezarat_cod_cod_reshteh_status_cod_asli_cod_payeh_ozviyat_no_324B9");

        Entity.HasIndex(e => new { e.Sex, e.StatusCod, e.OzviyatNo },
            "IX_tbl_engineers_sex_status_cod_ozviyat_no_6111E");

        Entity.HasIndex(e => new { e.Sex, e.StatusCod, e.OzviyatNo, e.Payeh },
            "IX_tbl_engineers_sex_status_cod_ozviyat_no_payeh_DAD82");

        Entity.HasIndex(e => new { e.StatusCod, e.CodReshteh, e.OzviyatNo, e.DNemayandegiCod },
            "IX_tbl_engineers_status_cod_cod_reshteh_ozviyat_no_d_nemayandegi_cod_D1CAE");

        Entity.HasIndex(e => new { e.StatusCod, e.CodReshteh, e.OzviyatNo, e.Payeh },
            "IX_tbl_engineers_status_cod_cod_reshteh_ozviyat_no_payeh_7E5D4");

        Entity.HasIndex(e => new { e.StatusCod, e.Fname, e.OzviyatNo, e.DNemayandegiCod },
            "IX_tbl_engineers_status_cod_fname_ozviyat_no_d_nemayandegi_cod_CCD84");

        Entity.HasIndex(e => new { e.StatusCod, e.OzviyatNo }, "IX_tbl_engineers_status_cod_ozviyat_no_14848");

        Entity.HasIndex(e => new { e.StatusCod, e.OzviyatNo }, "IX_tbl_engineers_status_cod_ozviyat_no_F3AB2");

        Entity.HasIndex(e => new { e.StatusCod, e.OzviyatNo, e.Payeh },
            "IX_tbl_engineers_status_cod_ozviyat_no_payeh_B8106");

        Entity.HasIndex(e => new { e.Id, e.StatusCod, e.OzviyatNo }, "idx1");

        Entity.Property(e => e.OzviyatNo)
            .ValueGeneratedNever()
            .HasColumnName("ozviyat_no");
        Entity.Property(e => e.Address)
            .HasMaxLength(250)
            .HasColumnName("address");
        Entity.Property(e => e.AddressState)
            .HasDefaultValue(true)
            .HasColumnName("address_state");
        Entity.Property(e => e.AsliCod).HasColumnName("asli_cod");
        Entity.Property(e => e.BornCity).HasColumnName("born_city");
        Entity.Property(e => e.BornDate)
            .HasMaxLength(50)
            .HasColumnName("born_date");
        Entity.Property(e => e.CodPostiJob)
            .HasMaxLength(12)
            .HasColumnName("cod_posti_job");
        Entity.Property(e => e.CodPostiManzel)
            .HasMaxLength(12)
            .HasColumnName("cod_posti_manzel");
        Entity.Property(e => e.CodReshteh).HasColumnName("cod_reshteh");
        Entity.Property(e => e.Comments)
            .HasMaxLength(250)
            .HasColumnName("comments");
        Entity.Property(e => e.Control).HasColumnName("control");
        Entity.Property(e => e.CoronaTamdidDat)
            .HasMaxLength(50)
            .HasColumnName("corona_tamdid_dat");
        Entity.Property(e => e.DNemayandegiCod).HasColumnName("d_nemayandegi_cod");
        Entity.Property(e => e.DaftarNo)
            .HasMaxLength(50)
            .HasColumnName("daftar_no");
        Entity.Property(e => e.EMail)
            .HasMaxLength(100)
            .HasColumnName("e_mail");
        Entity.Property(e => e.EndDate)
            .HasMaxLength(50)
            .HasColumnName("end_date");
        Entity.Property(e => e.EndDateTemp)
            .HasMaxLength(50)
            .HasColumnName("end_date_temp");
        Entity.Property(e => e.EndMetrajNezarat)
            .HasDefaultValue(0L)
            .HasColumnName("end_metraj_nezarat");
        Entity.Property(e => e.EndMetrajTarrahi)
            .HasDefaultValue(0L)
            .HasColumnName("end_metraj_tarrahi");
        Entity.Property(e => e.EndTedadNezarat)
            .HasDefaultValue(0)
            .HasColumnName("end_tedad_nezarat");
        Entity.Property(e => e.EndTedadTarrahi)
            .HasDefaultValue(0)
            .HasColumnName("end_tedad_tarrahi");
        Entity.Property(e => e.EnglishName)
            .HasMaxLength(50)
            .HasColumnName("english_name");
        Entity.Property(e => e.EntegalCodOstan).HasColumnName("entegal_cod_ostan");
        Entity.Property(e => e.EstelamDat)
            .HasMaxLength(50)
            .HasColumnName("estelam_dat");
        Entity.Property(e => e.EstelamNumber)
            .HasMaxLength(50)
            .HasColumnName("estelam_number");
        Entity.Property(e => e.EstelamResult)
            .HasDefaultValue(false)
            .HasColumnName("estelam_result");
        Entity.Property(e => e.FlCod).HasColumnName("fl_cod");
        Entity.Property(e => e.Fname)
            .HasMaxLength(50)
            .HasColumnName("fname");
        Entity.Property(e => e.FullName)
            .HasMaxLength(50)
            .HasColumnName("full_name");
        Entity.Property(e => e.Id)
            .ValueGeneratedOnAdd()
            .HasColumnName("id");
        Entity.Property(e => e.Idno)
            .HasMaxLength(50)
            .HasColumnName("idno");
        Entity.Property(e => e.Img)
            .HasColumnType("image")
            .HasColumnName("img");
        Entity.Property(e => e.JobAddress)
            .HasMaxLength(250)
            .HasColumnName("job_address");
        Entity.Property(e => e.JobAddressState)
            .HasDefaultValue(false)
            .HasColumnName("job_address_state");
        Entity.Property(e => e.KarbarId).HasColumnName("karbar_id");
        Entity.Property(e => e.Maghta).HasColumnName("maghta");
        Entity.Property(e => e.Mah).HasColumnName("mah");
        Entity.Property(e => e.Main)
            .HasMaxLength(50)
            .HasColumnName("main");
        Entity.Property(e => e.MelliCod)
            .HasMaxLength(15)
            .HasColumnName("melli_cod");
        Entity.Property(e => e.Military)
            .HasDefaultValue(0)
            .HasColumnName("military");
        Entity.Property(e => e.MobNo)
            .HasMaxLength(50)
            .HasDefaultValueSql("((0))")
            .HasColumnName("mob_no");
        Entity.Property(e => e.ModifiedDat)
            .HasDefaultValueSql("(getdate())")
            .HasColumnType("datetime")
            .HasColumnName("modified_dat");
        Entity.Property(e => e.Mojri).HasColumnName("mojri");
        Entity.Property(e => e.Moshaver)
            .HasDefaultValue(false)
            .HasColumnName("moshaver");
        Entity.Property(e => e.Name)
            .HasMaxLength(50)
            .HasColumnName("name");
        Entity.Property(e => e.NazerJaygozin)
            .HasMaxLength(150)
            .HasColumnName("nazer_jaygozin");
        Entity.Property(e => e.NazerMoghim)
            .HasDefaultValue(false)
            .HasColumnName("nazer_moghim");
        Entity.Property(e => e.NazerMoghimComment)
            .HasMaxLength(255)
            .HasColumnName("nazer_moghim_comment");
        Entity.Property(e => e.Ncontrol).HasColumnName("ncontrol");
        Entity.Property(e => e.OldControlMap).HasColumnName("old_control_map");
        Entity.Property(e => e.OldControlNezarat).HasColumnName("old_control_nezarat");
        Entity.Property(e => e.OstanCod).HasColumnName("ostan_cod");
        Entity.Property(e => e.OzvDate)
            .HasMaxLength(50)
            .HasColumnName("ozv_date");
        Entity.Property(e => e.OzvExpDate)
            .HasMaxLength(50)
            .HasColumnName("ozv_exp_date");
        Entity.Property(e => e.OzviyatMark)
            .HasMaxLength(50)
            .HasColumnName("ozviyat_mark");
        Entity.Property(e => e.ParvanehMark)
            .HasMaxLength(50)
            .HasColumnName("parvaneh_mark");
        Entity.Property(e => e.ParvanehNo).HasColumnName("parvaneh_no");
        Entity.Property(e => e.ParvanehOstan)
            .HasDefaultValue(14)
            .HasColumnName("parvaneh_ostan");
        Entity.Property(e => e.ParvanehSodoorDate)
            .HasMaxLength(50)
            .HasColumnName("parvaneh_sodoor_date");
        Entity.Property(e => e.ParvanehStatus)
            .HasDefaultValue(0)
            .HasColumnName("parvaneh_status");
        Entity.Property(e => e.Password)
            .HasMaxLength(50)
            .HasColumnName("password");
        Entity.Property(e => e.Payeh).HasColumnName("payeh");
        Entity.Property(e => e.PayehEjra)
            .HasDefaultValue(0)
            .HasColumnName("payeh_ejra");
        Entity.Property(e => e.PayehType)
            .HasDefaultValue(0)
            .HasColumnName("payeh_type");
        Entity.Property(e => e.Pname)
            .HasMaxLength(50)
            .HasColumnName("pname");
        Entity.Property(e => e.PnezaratCod).HasColumnName("pnezarat_cod");
        Entity.Property(e => e.PtarrahiCod).HasColumnName("ptarrahi_cod");
        Entity.Property(e => e.RelOzviyatNo)
            .HasMaxLength(15)
            .HasColumnName("rel_ozviyat_no");
        Entity.Property(e => e.RequestNezarat)
            .HasDefaultValue(false)
            .HasColumnName("request_nezarat");
        Entity.Property(e => e.ReshtehName)
            .HasMaxLength(50)
            .HasColumnName("reshteh_name");
        Entity.Property(e => e.Rooz).HasColumnName("rooz");
        Entity.Property(e => e.SabtDate)
            .HasMaxLength(50)
            .HasColumnName("sabt_date");
        Entity.Property(e => e.Sal).HasColumnName("sal");
        Entity.Property(e => e.Sex).HasColumnName("sex");
        Entity.Property(e => e.SignAllow)
            .HasDefaultValue(false)
            .HasColumnName("sign_allow");
        Entity.Property(e => e.StatusCod).HasColumnName("status_cod");
        Entity.Property(e => e.TaaholState)
            .HasDefaultValue(0)
            .HasColumnName("taahol_state");
        Entity.Property(e => e.TafkikActive)
            .HasDefaultValue(0)
            .HasColumnName("tafkik_active");
        Entity.Property(e => e.TafkikCert)
            .HasDefaultValue(false)
            .HasColumnName("tafkik_cert");
        Entity.Property(e => e.TamdidDate)
            .HasMaxLength(50)
            .HasColumnName("tamdid_date");
        Entity.Property(e => e.Tasviye).HasColumnName("tasviye");
        Entity.Property(e => e.TelJob)
            .HasMaxLength(50)
            .HasColumnName("tel_job");
        Entity.Property(e => e.TelNo)
            .HasMaxLength(50)
            .HasColumnName("tel_no");
        Entity.Property(e => e.University)
            .HasMaxLength(100)
            .HasColumnName("university");
        Entity.Property(e => e.UniversityCod)
            .HasDefaultValue(0)
            .HasColumnName("university_cod");
        Entity.Property(e => e.UniversityDate)
            .HasMaxLength(50)
            .HasColumnName("university_date");
        Entity.Property(e => e.Website)
            .HasMaxLength(50)
            .HasColumnName("website");
    }
}