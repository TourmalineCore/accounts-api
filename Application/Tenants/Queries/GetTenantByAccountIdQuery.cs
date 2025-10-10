using System.Threading.Tasks;
using Core.Contracts;

namespace Application.Tenants.Queries;

public readonly struct GetTenantByAccountIdQuery
{
  public long AccountId { get; init; }
}

public class GetTenantByAccountIdQueryHandler
{
  private readonly IAccountsRepository _accountsRepository;

  public GetTenantByAccountIdQueryHandler(IAccountsRepository accountsRepository)
  {
    _accountsRepository = accountsRepository;
  }

  public async Task<long> HandleAsync(GetTenantByAccountIdQuery query)
  {
    var account = await _accountsRepository.GetByIdAsync(query.AccountId);

    return account.TenantId;
  }
}
