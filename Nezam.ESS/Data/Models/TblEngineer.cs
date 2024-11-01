namespace Nezam.ESS.backend.Data.Models;

public class TblEngineer
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Fname { get; set; }

    public string? FullName { get; set; }

    public string? Pname { get; set; }

    public int? PtarrahiCod { get; set; }

    public int? PnezaratCod { get; set; }

    public int? CodReshteh { get; set; }

    public int? FlCod { get; set; }

    public string? ReshtehName { get; set; }

    public long? ParvanehNo { get; set; }

    public int? ParvanehStatus { get; set; }

    public long OzviyatNo { get; set; }

    public string? OzviyatMark { get; set; }

    public string? EndDate { get; set; }

    public bool? Control { get; set; }

    public string? Main { get; set; }

    public string? DaftarNo { get; set; }

    public byte[]? Img { get; set; }

    public int? OstanCod { get; set; }

    public string? Idno { get; set; }

    public string? BornDate { get; set; }

    public int? BornCity { get; set; }

    public int? ParvanehOstan { get; set; }

    public string? TamdidDate { get; set; }

    public string? ParvanehSodoorDate { get; set; }

    public bool? Ncontrol { get; set; }

    public string? Address { get; set; }

    public bool? AddressState { get; set; }

    public string? TelNo { get; set; }

    public string? MobNo { get; set; }

    public string? MelliCod { get; set; }

    public string? EMail { get; set; }

    public bool? OldControlMap { get; set; }

    public bool? OldControlNezarat { get; set; }

    public string? JobAddress { get; set; }

    public string? OzvDate { get; set; }

    public string? TelJob { get; set; }

    public int? Sex { get; set; }

    public string? University { get; set; }

    public int? UniversityCod { get; set; }

    public string? UniversityDate { get; set; }

    public string? Comments { get; set; }

    public int? Sal { get; set; }

    public int? Mah { get; set; }

    public int? Rooz { get; set; }

    public string? SabtDate { get; set; }

    public int? KarbarId { get; set; }

    public int? DNemayandegiCod { get; set; }

    public int? StatusCod { get; set; }

    public string? EnglishName { get; set; }

    public string? NazerJaygozin { get; set; }

    public int? EntegalCodOstan { get; set; }

    public string? OzvExpDate { get; set; }

    public int? AsliCod { get; set; }

    public int? Maghta { get; set; }

    public bool? Mojri { get; set; }

    public int? PayehType { get; set; }

    public int? Payeh { get; set; }

    public string? ParvanehMark { get; set; }

    public bool? Tasviye { get; set; }

    public bool? Moshaver { get; set; }

    public bool? NazerMoghim { get; set; }

    public string? NazerMoghimComment { get; set; }

    public long? EndMetrajNezarat { get; set; }

    public long? EndMetrajTarrahi { get; set; }

    public int? EndTedadNezarat { get; set; }

    public int? EndTedadTarrahi { get; set; }

    public bool? SignAllow { get; set; }

    public bool? RequestNezarat { get; set; }

    public bool? EstelamResult { get; set; }

    public string? EstelamDat { get; set; }

    public string? EstelamNumber { get; set; }

    public string? Password { get; set; }

    public string? Website { get; set; }

    public int? Military { get; set; }

    public string? CodPostiManzel { get; set; }

    public string? CodPostiJob { get; set; }

    public int? TaaholState { get; set; }

    public bool? JobAddressState { get; set; }

    public string? RelOzviyatNo { get; set; }

    public DateTime? ModifiedDat { get; set; }

    public int? PayehEjra { get; set; }

    public string? EndDateTemp { get; set; }

    public bool? TafkikCert { get; set; }

    public string? CoronaTamdidDat { get; set; }

    public int? TafkikActive { get; set; }

    // public virtual ICollection<TblAmoozeshCoursePresent> TblAmoozeshCoursePresents { get; set; } = new List<TblAmoozeshCoursePresent>();
    //
    // public virtual ICollection<TblAmoozeshInfo> TblAmoozeshInfos { get; set; } = new List<TblAmoozeshInfo>();
    //
    // public virtual TblBargBazrasin? TblBargBazrasin { get; set; }
    //
    // public virtual ICollection<TblBargMojrianMember> TblBargMojrianMembers { get; set; } = new List<TblBargMojrianMember>();
    //
    // public virtual ICollection<TblCommunity> TblCommunities { get; set; } = new List<TblCommunity>();
    //
    // public virtual ICollection<TblCommunityAdvocate> TblCommunityAdvocates { get; set; } = new List<TblCommunityAdvocate>();
    //
    // public virtual ICollection<TblCommunityLog> TblCommunityLogs { get; set; } = new List<TblCommunityLog>();
    //
    // public virtual ICollection<TblCompetence1> TblCompetence1s { get; set; } = new List<TblCompetence1>();
    //
    // public virtual ICollection<TblCompetenceAmoozeshCourseMember> TblCompetenceAmoozeshCourseMembers { get; set; } = new List<TblCompetenceAmoozeshCourseMember>();
    //
    // public virtual ICollection<TblCompetence> TblCompetences { get; set; } = new List<TblCompetence>();
    //
    // public virtual ICollection<TblDafaterMember> TblDafaterMembers { get; set; } = new List<TblDafaterMember>();
    //
    // public virtual ICollection<TblEducationInfo> TblEducationInfos { get; set; } = new List<TblEducationInfo>();
    //
    // public virtual ICollection<TblElection> TblElections { get; set; } = new List<TblElection>();
    //
    // public virtual ICollection<TblEngineersArkan> TblEngineersArkans { get; set; } = new List<TblEngineersArkan>();
    //
    // public virtual ICollection<TblEngineersAuthentication> TblEngineersAuthentications { get; set; } = new List<TblEngineersAuthentication>();
    //
    // public virtual ICollection<TblEngineersBimeMablagRoozane> TblEngineersBimeMablagRoozanes { get; set; } = new List<TblEngineersBimeMablagRoozane>();
    //
    // public virtual ICollection<TblEngineersBimePayment> TblEngineersBimePayments { get; set; } = new List<TblEngineersBimePayment>();
    //
    // public virtual TblEngineersBimeinfo? TblEngineersBimeinfo { get; set; }
    //
    // public virtual ICollection<TblEngineersKorsan> TblEngineersKorsans { get; set; } = new List<TblEngineersKorsan>();
    //
    // public virtual ICollection<TblEngineersSiteLog> TblEngineersSiteLogs { get; set; } = new List<TblEngineersSiteLog>();
    //
    // public virtual ICollection<TblErjaaNezaratEngBan> TblErjaaNezaratEngBans { get; set; } = new List<TblErjaaNezaratEngBan>();
    //
    // public virtual ICollection<TblErjaaNezaratMapCandid> TblErjaaNezaratMapCandids { get; set; } = new List<TblErjaaNezaratMapCandid>();
    //
    // public virtual ICollection<TblErjaaNezaratNoHozoor> TblErjaaNezaratNoHozoors { get; set; } = new List<TblErjaaNezaratNoHozoor>();
    //
    // public virtual ICollection<TblErjaaNezaratRejectPenalty> TblErjaaNezaratRejectPenalties { get; set; } = new List<TblErjaaNezaratRejectPenalty>();
    //
    // public virtual ICollection<TblErjaaNezaratRequest> TblErjaaNezaratRequests { get; set; } = new List<TblErjaaNezaratRequest>();
    //
    // public virtual ICollection<TblErjaaNezaratSahmiehChoice> TblErjaaNezaratSahmiehChoices { get; set; } = new List<TblErjaaNezaratSahmiehChoice>();
    //
    // public virtual ICollection<TblErjaaNezaratSahmiehTemp> TblErjaaNezaratSahmiehTemps { get; set; } = new List<TblErjaaNezaratSahmiehTemp>();
    //
    // public virtual ICollection<TblErjaaNezarat> TblErjaaNezarats { get; set; } = new List<TblErjaaNezarat>();
    //
    // public virtual ICollection<TblGasBazrasin> TblGasBazrasins { get; set; } = new List<TblGasBazrasin>();
    //
    // public virtual ICollection<TblGeoNezaratMember> TblGeoNezaratMembers { get; set; } = new List<TblGeoNezaratMember>();
    //
    // public virtual ICollection<TblGeoSahmieh> TblGeoSahmiehs { get; set; } = new List<TblGeoSahmieh>();
    //
    // public virtual ICollection<TblLegalMembersMember> TblLegalMembersMembers { get; set; } = new List<TblLegalMembersMember>();
    //
    // public virtual ICollection<TblMadde27Eng> TblMadde27Engs { get; set; } = new List<TblMadde27Eng>();
    //
    // public virtual ICollection<TblMadde27EngsLog> TblMadde27EngsLogs { get; set; } = new List<TblMadde27EngsLog>();
    //
    // public virtual ICollection<TblMadde27EngsSalahiyat> TblMadde27EngsSalahiyats { get; set; } = new List<TblMadde27EngsSalahiyat>();
    //
    // public virtual ICollection<TblMadde27EngsScope> TblMadde27EngsScopes { get; set; } = new List<TblMadde27EngsScope>();
    //
    // public virtual ICollection<TblMadde27Penalty> TblMadde27Penalties { get; set; } = new List<TblMadde27Penalty>();
    //
    // public virtual ICollection<TblMadde27Sahmieh> TblMadde27Sahmiehs { get; set; } = new List<TblMadde27Sahmieh>();
    //
    // public virtual ICollection<TblMainParvanehFish> TblMainParvanehFishes { get; set; } = new List<TblMainParvanehFish>();
    //
    // public virtual ICollection<TblMainParvaneh> TblMainParvanehs { get; set; } = new List<TblMainParvaneh>();
    //
    // public virtual ICollection<TblMaininfoTarrahHozoor> TblMaininfoTarrahHozoors { get; set; } = new List<TblMaininfoTarrahHozoor>();
    //
    // public virtual TblMaliEngineer? TblMaliEngineer { get; set; }
    //
    // public virtual ICollection<TblMaliEngineersTran> TblMaliEngineersTrans { get; set; } = new List<TblMaliEngineersTran>();
    //
    // public virtual ICollection<TblMaliHasabNoLog> TblMaliHasabNoLogs { get; set; } = new List<TblMaliHasabNoLog>();
    //
    // public virtual ICollection<TblMaliHasabNo> TblMaliHasabNos { get; set; } = new List<TblMaliHasabNo>();
    //
    // public virtual ICollection<TblMaliVamRequest> TblMaliVamRequests { get; set; } = new List<TblMaliVamRequest>();
    //
    // public virtual ICollection<TblMaliVam> TblMaliVams { get; set; } = new List<TblMaliVam>();
    //
    // public virtual ICollection<TblMaliyatPardakhtiSayer> TblMaliyatPardakhtiSayers { get; set; } = new List<TblMaliyatPardakhtiSayer>();
    //
    // public virtual ICollection<TblMaliyatPardakhti> TblMaliyatPardakhtis { get; set; } = new List<TblMaliyatPardakhti>();
    //
    // public virtual ICollection<TblMapControlDastoorMember> TblMapControlDastoorMembers { get; set; } = new List<TblMapControlDastoorMember>();
    //
    // public virtual ICollection<TblMapControllerMember> TblMapControllerMembers { get; set; } = new List<TblMapControllerMember>();
    //
    // public virtual ICollection<TblMojrianMember> TblMojrianMembers { get; set; } = new List<TblMojrianMember>();
    //
    // public virtual ICollection<TblMojrianSarparastKargahBan> TblMojrianSarparastKargahBans { get; set; } = new List<TblMojrianSarparastKargahBan>();
    //
    // public virtual ICollection<TblMojrianSarparastKargah> TblMojrianSarparastKargahs { get; set; } = new List<TblMojrianSarparastKargah>();
    //
    // public virtual ICollection<TblMoshaverinMember> TblMoshaverinMembers { get; set; } = new List<TblMoshaverinMember>();
    //
    // public virtual ICollection<TblNcontBazdidMali> TblNcontBazdidMalis { get; set; } = new List<TblNcontBazdidMali>();
    //
    // public virtual ICollection<TblNcontBazdid> TblNcontBazdids { get; set; } = new List<TblNcontBazdid>();
    //
    // public virtual ICollection<TblNcontEngActiveLog> TblNcontEngActiveLogs { get; set; } = new List<TblNcontEngActiveLog>();
    //
    // public virtual ICollection<TblNcontEng> TblNcontEngs { get; set; } = new List<TblNcontEng>();
    //
    // public virtual ICollection<TblPayankarlist> TblPayankarlists { get; set; } = new List<TblPayankarlist>();
    //
    // public virtual ICollection<TblPishnahadat> TblPishnahadats { get; set; } = new List<TblPishnahadat>();
    //
    // public virtual ICollection<TblRestriction> TblRestrictions { get; set; } = new List<TblRestriction>();
    //
    // public virtual ICollection<TblSabtenamEntekhabat> TblSabtenamEntekhabats { get; set; } = new List<TblSabtenamEntekhabat>();
    //
    // public virtual ICollection<TblSabtenamRoozemohandesi> TblSabtenamRoozemohandesis { get; set; } = new List<TblSabtenamRoozemohandesi>();
    //
    // public virtual ICollection<TblSahmiehControl> TblSahmiehControls { get; set; } = new List<TblSahmiehControl>();
    //
    // public virtual ICollection<TblSahmieh> TblSahmiehs { get; set; } = new List<TblSahmieh>();
    //
    // public virtual ICollection<TblSahmiyeFullTime> TblSahmiyeFullTimes { get; set; } = new List<TblSahmiyeFullTime>();
    //
    // public virtual ICollection<TblSahmiyeSaliane> TblSahmiyeSalianes { get; set; } = new List<TblSahmiyeSaliane>();
    //
    // public virtual ICollection<TblShakhesPenalty> TblShakhesPenalties { get; set; } = new List<TblShakhesPenalty>();
    //
    // public virtual ICollection<TblShakhesPenalty3Reshteh> TblShakhesPenalty3Reshtehs { get; set; } = new List<TblShakhesPenalty3Reshteh>();
    //
    // public virtual ICollection<TblSodoorCartReport> TblSodoorCartReports { get; set; } = new List<TblSodoorCartReport>();
    //
    // public virtual ICollection<TblSodoorFormList> TblSodoorFormLists { get; set; } = new List<TblSodoorFormList>();
    //
    // public virtual ICollection<TblSodoorParvanehTahvil> TblSodoorParvanehTahvils { get; set; } = new List<TblSodoorParvanehTahvil>();
    //
    // public virtual ICollection<TblSurvey> TblSurveys { get; set; } = new List<TblSurvey>();
    //
    // public virtual ICollection<TblTafkikEngineer> TblTafkikEngineers { get; set; } = new List<TblTafkikEngineer>();
    //
    public virtual ICollection<TblUtmEng> TblUtmEngs { get; set; } = new List<TblUtmEng>();
    //
    // public virtual ICollection<TblVaccination> TblVaccinations { get; set; } = new List<TblVaccination>();
    //
    // public virtual ICollection<ZtblMaliPerson> ZtblMaliPeople { get; set; } = new List<ZtblMaliPerson>();
}