using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Accounts.Application.Contracts;
using Accounts.Application.Validators;
using Accounts.Core.Contracts;
using FluentValidation;

namespace Accounts.Application.Users.Commands
{
    public class AccountUpdateCommand
    {
        public long Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string? MiddleName { get; set; }

        public List<long> Roles { get; set; }
    }

    public class AccountUpdateCommandHandler : ICommandHandler<AccountUpdateCommand>
    {
        private readonly IAccountRepository _accountRepository;
        private readonly AccountUpdateCommandValidator _validator;

        public AccountUpdateCommandHandler(IAccountRepository accountRepository, AccountUpdateCommandValidator accountUpdateCommandValidator)
        {
            _accountRepository = accountRepository;
            _validator = accountUpdateCommandValidator;
        }

        public async Task Handle(AccountUpdateCommand command)
        {
            var validationResult = await _validator.ValidateAsync(command);

            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors[0].ErrorMessage);
            }

            var account = await _accountRepository.FindByIdAsync(command.Id);

            if (account == null)
            {
                throw new NullReferenceException("Account not found");
            }

            account.Update(
                    command.FirstName,
                    command.LastName,
                    command.MiddleName,
                    command.Roles
                );

            await _accountRepository.UpdateAsync(account);
        }
    }
}