using System.Collections.Generic;
using System.Linq;
using Application.Users.Commands;
using Core.Contracts;
using FluentValidation;

namespace Application.Validators;

public class AccountUpdateCommandValidator : AbstractValidator<AccountUpdateCommand>
{
    public AccountUpdateCommandValidator(IRoleRepository roleRepository)
    {
        RuleFor(x => x.Roles)
            .NotNull()
            .NotEmpty()
            .Must(IsRoleIdsUnique)
            .MustAsync(
                    async (accountRoleIds, _) =>
                    {
                        var roles = await roleRepository.GetRolesAsync();
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