namespace Nezam.Modular.ESS.Secretariat.Domain.Documents.Exceptions;

public class ReferralAlreadyRespondedException : BonDomainException
{
    public ReferralAlreadyRespondedException(string message = "Referral has already been responded to.", string? errorCode = null, object? parameters = null)
        : base(message, errorCode ?? GenerateErrorCode(nameof(ReferralAlreadyRespondedException)), parameters) { }

    private static string GenerateErrorCode(string exceptionName)
    {
        return exceptionName.ToUpper().Replace(" ", "_");
    }
}