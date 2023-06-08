using Application.Accounts.Commands;
using Core.Contracts;
using FluentValidation;

namespace Application.Accounts.Validators;

public class AccountUpdateCommandValidator : AbstractValidator<AccountUpdateCommand>
{
    public AccountUpdateCommandValidator(IRolesRepository rolesRepository)
    {
        RuleFor(x => x.FirstName).MaximumLength(50);
        RuleFor(x => x.LastName).MaximumLength(50);
        RuleFor(x => x.MiddleName).MaximumLength(50);
        RuleFor(x => x.Roles).RolesMustBeValid(rolesRepository);
    }
}