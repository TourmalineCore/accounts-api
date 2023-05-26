using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Accounts.Validators;
using Application.Contracts;
using Core.Contracts;
using FluentValidation;

namespace Application.Accounts.Commands;

public readonly struct AccountUpdateCommand
{
    public long Id { get; init; }

    public string FirstName { get; init; }

    public string LastName { get; init; }

    public string? MiddleName { get; init; }

    public List<long> Roles { get; init; }
}

public class AccountUpdateCommandHandler : ICommandHandler<AccountUpdateCommand>
{
    private readonly IAccountsRepository _accountsRepository;
    private readonly AccountUpdateCommandValidator _validator;

    public AccountUpdateCommandHandler(IAccountsRepository accountsRepository, AccountUpdateCommandValidator accountUpdateCommandValidator)
    {
        _accountsRepository = accountsRepository;
        _validator = accountUpdateCommandValidator;
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

        account.Update(
                command.FirstName,
                command.LastName,
                command.MiddleName,
                command.Roles
            );

        await _accountsRepository.UpdateAsync(account);
    }
}