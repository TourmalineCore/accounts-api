using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Accounts.Validators;
using Application.Contracts;
using Application.HttpClients;
using Core.Contracts;
using Core.Entities;
using FluentValidation;

namespace Application.Accounts.Commands;

public readonly struct AccountCreationCommand
{
    public string FirstName { get; init; }

    public string LastName { get; init; }

    public string? MiddleName { get; init; }

    public string CorporateEmail { get; init; }

    public List<long> RoleIds { get; init; }

    public long TenantId { get; init; }
}

public class AccountCreationCommandHandler : ICommandHandler<AccountCreationCommand, long>
{
    private readonly IAccountsRepository _accountsRepository;
    private readonly IRolesRepository _rolesRepository;
    private readonly AccountCreationCommandValidator _validator;
    private readonly IHttpClient _httpClient;

    public AccountCreationCommandHandler(
        IAccountsRepository accountsRepository,
        AccountCreationCommandValidator validator,
        IHttpClient httpClient,
        IRolesRepository rolesRepository)
    {
        _accountsRepository = accountsRepository;
        _validator = validator;
        _httpClient = httpClient;
        _rolesRepository = rolesRepository;
    }

    public async Task<long> HandleAsync(AccountCreationCommand command)
    {
        var validationResult = await _validator.ValidateAsync(command);

        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors[0].ErrorMessage);
        }

        var roles = await _rolesRepository.GetAllAsync();
        var newAccountRoles = roles.Where(x => command.RoleIds.Contains(x.Id));

        var account = new Account(
                command.CorporateEmail,
                command.FirstName,
                command.LastName,
                command.MiddleName,
                newAccountRoles,
                command.TenantId
            );

        var accountId = await _accountsRepository.CreateAsync(account);

        await _httpClient.SendRequestToRegisterNewAccountAsync(accountId, account.CorporateEmail);

        await _httpClient.SendRequestToCreateNewEmployeeAsync(
                command.CorporateEmail,
                command.FirstName,
                command.LastName,
                command.MiddleName,
                command.TenantId
            );

        return accountId;
    }
}