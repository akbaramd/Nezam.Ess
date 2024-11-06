using Bonyan.Layer.Domain.Exceptions;

namespace Nezam.Modular.ESS.Secretariat.Domain.Documents.Exceptions;

public class ReferralAlreadyRespondedException : DomainException
{
    public ReferralAlreadyRespondedException(string message = "Referral has already been responded to.", string? errorCode = null, object? parameters = null)
        : base(message, errorCode ?? GenerateErrorCode(nameof(ReferralAlreadyRespondedException)), parameters) { }

    private static string GenerateErrorCode(string exceptionName)
    {
        return exceptionName.ToUpper().Replace(" ", "_");
    }
}