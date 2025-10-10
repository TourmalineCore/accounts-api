using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Contracts;
using Core.Contracts;

namespace Application.Accounts.Queries;

public readonly struct GetPermissionsByAccountIdQuery
{
  public long Id { get; init; }
}

public class GetPermissionsByAccountIdQueryHandler : IQueryHandler<GetPermissionsByAccountIdQuery, IEnumerable<string>>
{
  private readonly IAccountsRepository _accountsRepository;

  public GetPermissionsByAccountIdQueryHandler(IAccountsRepository accountsRepository)
  {
    _accountsRepository = accountsRepository;
  }

  public async Task<IEnumerable<string>> HandleAsync(GetPermissionsByAccountIdQuery query)
  {
    var account = await _accountsRepository.GetByIdAsync(query.Id);

    return account
      .AccountRoles
      .Select(x => x.Role)
      .SelectMany(x => x.Permissions)
      .Distinct();
  }
}
