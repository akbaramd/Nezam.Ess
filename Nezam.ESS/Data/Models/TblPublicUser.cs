using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace Nezam.ESS.backend.Data.Models;

public class TblPublicUser
{
    public int Id { get; set; }

    public string UserName { get; set; } = default!;

    [JsonIgnore] [IgnoreDataMember] public string Password { get; set; } = default!;

    [JsonIgnore] [IgnoreDataMember] public Guid SecurityStamp { get; set; }

    [JsonIgnore] [IgnoreDataMember] public string? VerificationCode { get; set; }

    public string FirstName { get; set; } = default!;

    public string LastName { get; set; } = default!;

    public string MobNo { get; set; } = default!;

    public string Mellicode { get; set; } = default!;

    public int DNemayandegiCod { get; set; }
    public int AccessLevel { get; set; }

    public DateTime SabtDat { get; set; }
}