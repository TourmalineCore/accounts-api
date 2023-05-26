using System.Threading.Tasks;
using Application.Contracts;
using Application.HttpClients;
using Core.Contracts;

namespace Application.Accounts.Commands;

public readonly struct AccountUnblockCommand
{
    public long Id { get; init; }
}

public class AccountUnblockCommandHandler : ICommandHandler<AccountUnblockCommand>
{
    private readonly IAccountsRepository _accountsRepository;
    private readonly IHttpClient _httpClient;

    public AccountUnblockCommandHandler(IAccountsRepository accountsRepository, IHttpClient httpClient)
    {
        _accountsRepository = accountsRepository;
        _httpClient = httpClient;
    }

    public async Task HandleAsync(AccountUnblockCommand command)
    {
        var account = await _accountsRepository.GetByIdAsync(command.Id);
        account.Unblock();
        await _accountsRepository.UpdateAsync(account);
        await _httpClient.SendRequestToUnblockUserAsync(command.Id);
    }
}