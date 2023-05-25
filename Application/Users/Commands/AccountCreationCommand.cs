using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Contracts;
using Application.HttpClients;
using Application.Validators;
using Core.Contracts;
using Core.Entities;
using FluentValidation;

namespace Application.Users.Commands
{
    public class AccountCreationCommand
    {
        public string FirstName { get; init; }

        public string LastName { get; init; }

        public string? MiddleName { get; init; }

        public string CorporateEmail { get; init; }

        public List<long> RoleIds { get; init; }
    }

    public class AccountCreationCommandHandler : ICommandHandler<AccountCreationCommand, long>
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly AccountCreationCommandValidator _validator;
        private readonly IHttpClient _httpClient;

        public AccountCreationCommandHandler(
            IAccountRepository accountRepository,
            AccountCreationCommandValidator validator,
            IHttpClient httpClient,
            IRoleRepository roleRepository)
        {
            _accountRepository = accountRepository;
            _validator = validator;
            _httpClient = httpClient;
            _roleRepository = roleRepository;
        }

        public async Task<long> HandleAsync(AccountCreationCommand command)
        {
            var validationResult = await _validator.ValidateAsync(command);

            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors[0].ErrorMessage);
            }

            var roles = await _roleRepository.GetAllAsync();
            var newAccountRoles = roles.Where(x => command.RoleIds.Contains(x.Id));

            var account = new Account(command.CorporateEmail, command.FirstName, command.LastName, command.MiddleName, newAccountRoles);

            var accountId = await _accountRepository.CreateAsync(account);

            await _httpClient.SendRequestToRegisterNewAccountAsync(accountId, account.CorporateEmail);
            await _httpClient.SendRequestToCreateNewEmployeeAsync(command.CorporateEmail, command.FirstName,
                command.LastName, command.MiddleName);

            return accountId;
        }
    }
}