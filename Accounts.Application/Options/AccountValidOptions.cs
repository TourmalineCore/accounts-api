namespace Accounts.Application.Options;

public class AccountValidationOptions
{
    public string CorporateEmailDomain { get; set; }

    public bool IgnoreCorporateDomainValidationRule { get; set; }
}