using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Contracts;
using Core.Contracts;

namespace Application.Accounts.Queries;

public struct GetAccountsQuery
{
  public string CallerCorporateEmail { get; init; }
}

public class GetAccountsQueryHandler : IQueryHandler<GetAccountsQuery, IEnumerable<AccountDto>>
{
  private readonly IAccountsRepository _accountsRepository;

  public GetAccountsQueryHandler(IAccountsRepository accountsRepository)
  {
    _accountsRepository = accountsRepository;
  }

  public async Task<IEnumerable<AccountDto>> HandleAsync(GetAccountsQuery query)
  {
    var accounts = await _accountsRepository.GetAllAsync();
    return accounts.Select(account => new AccountDto(account, query.CallerCorporateEmail));
  }
}
