using System.Threading.Tasks;
using Accounts.Application.HttpClients;
using Accounts.Core.Contracts;

namespace Accounts.Application.Users.Commands;

public class AccountBlockCommand
{
    private readonly IAccountRepository _accountRepository;
    private readonly IHttpClient _httpClient;

    public AccountBlockCommand(IAccountRepository accountRepository, IHttpClient httpClient)
    {
        _accountRepository = accountRepository;
        _httpClient = httpClient;
    }

    public async Task Handle(long accountId)
    {
        var account = await _accountRepository.GetByIdAsync(accountId);
        account.Block();
        await _accountRepository.UpdateAsync(account);
        await _httpClient.SendRequestToBlockUserAsync(accountId);
    }
}