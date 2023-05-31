using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Accounts.Validators;
using Application.Contracts;
using Core.Contracts;
using FluentValidation;

namespace Application.Accounts.Commands;

public class AccountUpdateCommand
{
    public long Id { get; init; }

    public string FirstName { get; init; }

    public string LastName { get; init; }

    public string? MiddleName { get; init; }

    public List<long> Roles { get; init; }

    public string CallerCorporateEmail { get; set; }
}

public class AccountUpdateCommandHandler : ICommandHandler<AccountUpdateCommand>
{
    private readonly IAccountsRepository _accountsRepository;
    private readonly IRolesRepository _rolesRepository;
    private readonly AccountUpdateCommandValidator _validator;

    public AccountUpdateCommandHandler(IAccountsRepository accountsRepository, AccountUpdateCommandValidator accountUpdateCommandValidator, IRolesRepository rolesRepository)
    {
        _accountsRepository = accountsRepository;
        _validator = accountUpdateCommandValidator;
        _rolesRepository = rolesRepository;
    }

    public async Task HandleAsync(AccountUpdateCommand command)
    {
        var validationResult = await _validator.ValidateAsync(command);

        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors[0].ErrorMessage);
        }

        var account = await _accountsRepository.FindByIdAsync(command.Id);

        if (account == null)
        {
            throw new NullReferenceException("Account not found");
        }

        var newAccountRoles = await _rolesRepository.FindAsync(command.Roles);

        account.Update(
                command.FirstName,
                command.LastName,
                command.MiddleName,
                newAccountRoles,
                command.CallerCorporateEmail
            );

        await _accountsRepository.UpdateAsync(account);
    }
}