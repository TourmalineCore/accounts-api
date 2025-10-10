using Application.Accounts.Commands;
using Application.Options;
using Core.Contracts;
using FluentValidation;
using Microsoft.Extensions.Options;

namespace Application.Accounts.Validators;

public class AccountCreationCommandValidator : AbstractValidator<AccountCreationCommand>
{
  private readonly AccountValidationOptions _accountValidOptions;

  public AccountCreationCommandValidator(IRolesRepository rolesRepository, IAccountsRepository accountsRepository, IOptions<AccountValidationOptions> accountValidOptions)
  {
    _accountValidOptions = accountValidOptions.Value;

    RuleFor(x => x.FirstName).MaximumLength(50);
    RuleFor(x => x.LastName).MaximumLength(50);
    RuleFor(x => x.MiddleName).MaximumLength(50);
    When(_ => !_accountValidOptions.IgnoreCorporateDomainValidationRule,
      () =>
      {
        RuleFor(x => x.CorporateEmail)
          .NotNull()
          .NotEmpty()
          .EmailAddress()
          .Must(IsCorporateEmail)
          .MustAsync(
            async (corporateEmail, _) =>
            {
              var account = await accountsRepository.FindByCorporateEmailAsync(corporateEmail);
              return account == null;
            }
          )
          .WithMessage(x => $"Account with corporate email [{x.CorporateEmail}] already exists");
      }
    );

    RuleFor(x => x.RoleIds).RolesMustBeValid(rolesRepository);
  }

  private bool IsCorporateEmail(string corporateEmail)
  {
    return corporateEmail.Contains(_accountValidOptions.CorporateEmailDomain);
  }
}
