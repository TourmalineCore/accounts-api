using System.Collections.Generic;
using Core.Contracts;
using FluentValidation;

namespace Application.Accounts.Validators;

public static class AccountValidationExtensions
{
  public static void RolesMustBeValid<T>(this IRuleBuilder<T, List<long>> ruleBuilder, IRolesRepository rolesRepository)
  {
    ruleBuilder.SetAsyncValidator(new RolesValidator<T>(rolesRepository));
  }
}
