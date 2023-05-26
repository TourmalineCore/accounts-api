using System.Collections.Generic;
using System.Linq;
using Application.Accounts.Commands;
using Core.Contracts;
using FluentValidation;

namespace Application.Accounts.Validators;

public class AccountUpdateCommandValidator : AbstractValidator<AccountUpdateCommand>
{
    public AccountUpdateCommandValidator(IRolesRepository rolesRepository)
    {
        RuleFor(x => x.Roles)
            .NotNull()
            .NotEmpty()
            .Must(IsRoleIdsUnique)
            .MustAsync(
                    async (accountRoleIds, _) =>
                    {
                        var roles = await rolesRepository.GetRolesAsync();
                        var roleIds = roles.Select(x => x.Id);

                        return accountRoleIds.All(x => roleIds.Contains(x));
                    }
                )
            .WithMessage("Incorrect role ids. Probably you tried to set unavailable role id");
    }

    private static bool IsRoleIdsUnique(List<long> roleIds)
    {
        var uniqueRoleIds = roleIds.Distinct().ToList();
        return roleIds.Count == uniqueRoleIds.Count;
    }
}