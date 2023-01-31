using System.Linq;
using Accounts.Application.Users.Commands;
using Accounts.Core.Contracts;
using FluentValidation;

namespace Accounts.Application.Validators
{
    public class AccountCreationCommandValidator : AbstractValidator<AccountCreationCommand>
    {
        public AccountCreationCommandValidator(IRoleRepository roleRepository, IAccountRepository accountRepository)
        {
            RuleFor(x => x.CorporateEmail)
                .NotNull()
                .NotEmpty()
                .EmailAddress()
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
                .MustAsync(
                    async (accountRoleIds, _) =>
                    {
                        var roles = await roleRepository.GetRolesAsync();
                        var roleIds = roles.Select(x => x.Id);

                        return accountRoleIds.All(x => roleIds.Contains(x));
                    })
                .WithMessage("Incorrect role ids. Probably you tried to set unavailable role id");
        }
    }
}