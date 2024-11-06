using Bonyan.Layer.Domain.Exceptions;

namespace Nezam.Modular.ESS.Secretariat.Domain.Documents.Exceptions;

public class ReferralCanceledException : DomainException
{
    public ReferralCanceledException(string message = "Referral has been canceled and cannot be responded to again.", string? errorCode = null, object? parameters = null)
        : base(message, errorCode ?? GenerateErrorCode(nameof(ReferralCanceledException)), parameters) { }

    private static string GenerateErrorCode(string exceptionName)
    {
        return exceptionName.ToUpper().Replace(" ", "_");
    }
}