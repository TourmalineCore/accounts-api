using System.Threading.Tasks;
using Application.Contracts;
using Application.HttpClients;
using Core.Contracts;

namespace Application.Accounts.Commands;

public readonly struct AccountBlockCommand
{
  public long Id { get; init; }

  public string CallerCorporateEmail { get; init; }
}

public class AccountBlockCommandHandler : ICommandHandler<AccountBlockCommand>
{
  private readonly IAccountsRepository _accountsRepository;
  private readonly IHttpClient _httpClient;

  public AccountBlockCommandHandler(
    IAccountsRepository accountsRepository,
    IHttpClient httpClient
  )
  {
    _accountsRepository = accountsRepository;
    _httpClient = httpClient;
  }

  public async Task HandleAsync(AccountBlockCommand command)
  {
    var account = await _accountsRepository.GetByIdAsync(command.Id);
    account.Block(command.CallerCorporateEmail);
    await _accountsRepository.UpdateAsync(account);
    await _httpClient.SendRequestToBlockUserAsync(command.Id);
  }
}
