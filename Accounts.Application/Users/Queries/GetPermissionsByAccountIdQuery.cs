using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Accounts.Core.Contracts;

namespace Accounts.Application.Users.Queries;

public class GetPermissionsByAccountIdQueryHandler
{
    private readonly IAccountRepository _accountRepository;

    public GetPermissionsByAccountIdQueryHandler(IAccountRepository accountRepository)
    {
        _accountRepository = accountRepository;
    }

    public async Task<IEnumerable<string>> Handle(long accountId)
    {
        var account = await _accountRepository.GetByIdAsync(accountId);

        return account.AccountRoles
            .Select(x => x.Role)
            .SelectMany(x => x.Permissions)
            .Distinct();
    }
}