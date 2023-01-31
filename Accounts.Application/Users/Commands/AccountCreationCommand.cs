using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Accounts.Application.Contracts;
using Accounts.Application.HttpClients;
using Accounts.Core.Contracts;
using Accounts.Core.Entities;
using FluentValidation;

namespace Accounts.Application.Users.Commands
{
    public readonly struct AccountCreationCommand
    {
        public string FirstName { get; init; }

        public string LastName { get; init; }

        public string CorporateEmail { get; init; }

        public List<long> RoleIds { get; init; }
    }

    public class AccountCreationCommandHandler : ICommandHandler<AccountCreationCommand, long>
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IValidator<AccountCreationCommand> _validator;
        private readonly IHttpClient _httpClient;

        public AccountCreationCommandHandler(
            IAccountRepository accountRepository,
            IValidator<AccountCreationCommand> validator,
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

            var account = new Account(command.CorporateEmail, command.FirstName, command.LastName, newAccountRoles);

            var accountId = await _accountRepository.CreateAsync(account);

            await _httpClient.SendRequestToRegisterNewAccountAsync(accountId, account.CorporateEmail);

            return accountId;
        }
    }
}