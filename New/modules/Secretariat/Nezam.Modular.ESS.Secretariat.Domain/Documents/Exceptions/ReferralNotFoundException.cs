namespace Nezam.Modular.ESS.Secretariat.Domain.Documents.Exceptions
{
    public class ReferralNotFoundException : BonDomainException
    {
        public ReferralNotFoundException(string message = "Referral not found.", string? errorCode = null, object? parameters = null)
            : base(message, errorCode ?? GenerateErrorCode(nameof(ReferralNotFoundException)), parameters) { }

        private static string GenerateErrorCode(string exceptionName)
        {
            return exceptionName.ToUpper().Replace(" ", "_");
        }
    }
}