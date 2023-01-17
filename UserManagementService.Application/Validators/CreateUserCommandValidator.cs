using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManagementService.Application.Users.Commands;
using UserManagementService.Core.Entities;

namespace UserManagementService.Application.Validators
{
    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator()
        {
            RuleFor(el => el.CorporateEmail).NotNull().EmailAddress().NotEmpty();
            RuleFor(el => el.RoleId).NotNull().NotEmpty().Must(CompareRole);
        }
        private bool CompareRole(int roleId)
        {
            return Enum.IsDefined(typeof(RoleNames), roleId);
        }
    }
}
