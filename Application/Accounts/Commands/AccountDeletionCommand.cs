using System.Threading.Tasks;
using Application.Contracts;
using Application.HttpClients;
using Core.Contracts;

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

    await _httpClient.SendRequestToDeleteAccountAsync(account.CorporateEmail, accessToken);
    await _httpClient.SendRequestToDeleteEmployeeAsync(account.CorporateEmail, accessToken);
  }
}
