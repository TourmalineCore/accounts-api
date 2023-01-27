using Accounts.Application.Users.Commands;
using Accounts.Core.Entities;
using FluentValidation;
using System;

namespace Accounts.Application.Validators
{
    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator()
        {
            RuleFor(el => el.CorporateEmail).NotNull().EmailAddress().NotEmpty();
            RuleFor(el => el.RoleIds).NotNull().NotEmpty().Must(CompareRole);
        }
        private bool CompareRole(int[] roleIds)
        {
            // ToDo добавить норм валидацию
            return Enum.IsDefined(typeof(RoleNames), roleIds);
        }
    }
}
