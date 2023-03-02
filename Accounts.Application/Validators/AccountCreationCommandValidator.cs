using System.Collections.Generic;
using System.Linq;
using Accounts.Application.Options;
using Accounts.Application.Users.Commands;
using Accounts.Core.Contracts;
using FluentValidation;
using Microsoft.Extensions.Options;

namespace Accounts.Application.Validators
{
    public class AccountCreationCommandValidator : AbstractValidator<AccountCreationCommand>
    {
        private readonly AccountValidationOptions _accountValidOptions;
        public AccountCreationCommandValidator(IRoleRepository roleRepository, IAccountRepository accountRepository, IOptions<AccountValidationOptions> accountValidOptions)
        {
            _accountValidOptions = accountValidOptions.Value;

            RuleFor(x => x.CorporateEmail)
                .NotNull()
                .NotEmpty()
                .EmailAddress()
                .Must(IsCorporateEmail)
                .MustAsync(
                    async (corporateEmail, _) =>
                    {
                        var account = await accountRepository.FindByCorporateEmailAsync(corporateEmail);
                        return account == null;
                    })
                .WithMessage(x => $"Account with corporate email [{x.CorporateEmail}] already exists");

            RuleFor(x => x.RoleIds)
                .NotNull()
                .NotEmpty()
                .Must(IsRoleIdsUnique)
                .MustAsync(
                    async (accountRoleIds, _) =>
                    {
                        var roles = await roleRepository.GetRolesAsync();
                        var roleIds = roles.Select(x => x.Id);

                        return accountRoleIds.All(x => roleIds.Contains(x));
                    })
                .WithMessage("Incorrect role ids. Probably you tried to set unavailable role id");
        }

        private bool IsRoleIdsUnique(List<long> roleIds)
        {
            var uniqueRoleIds = roleIds.Distinct().ToList();
            return roleIds.Count == uniqueRoleIds.Count;
        }

        private bool IsCorporateEmail(string corporateEmail)
        {
            return corporateEmail.Contains(_accountValidOptions.CorporateEmailDomain);
        }
    }
}