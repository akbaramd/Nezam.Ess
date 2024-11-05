using Bonyan.Layer.Domain.Enumerations;

public class ReferralStatus : Enumeration
{
    public static readonly ReferralStatus New = new ReferralStatus(0, nameof(New));
    public static readonly ReferralStatus Viewed = new ReferralStatus(1, nameof(Viewed));
    public static readonly ReferralStatus Responded = new ReferralStatus(2, nameof(Responded));
    public static readonly ReferralStatus Canceled = new ReferralStatus(3, nameof(Canceled)); // وضعیت جدید

    public ReferralStatus(int id, string name) : base(id, name) { }
}