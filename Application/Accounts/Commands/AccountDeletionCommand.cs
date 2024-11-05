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

public struct AccountDeletionCommand
{
    public string CorporateEmail { get; init; }

    public string AccessToken { get; set; }
}

public class AccountDeletionCommandHandler : ICommandHandler<string, AccountDeletionCommand>
{
    private readonly IAccountsRepository _accountsRepository;
    private readonly IHttpClient _httpClient;

    public AccountDeletionCommandHandler(
        IAccountsRepository accountsRepository,
        IHttpClient httpClient
        )
    {
        _accountsRepository = accountsRepository;
        _httpClient = httpClient;
    }

    public async Task HandleAsync(string accessToken, AccountDeletionCommand command)
    {
        var account = await _accountsRepository.FindByCorporateEmailAsync(command.CorporateEmail);

        await _accountsRepository.DeleteAsync(account);

        await _httpClient.SendRequestToDeleteAccountAsync(command.AccessToken, account.CorporateEmail);
        await _httpClient.SendRequestToDeleteEmployeeAsync(command.AccessToken, command.CorporateEmail);
    }
}