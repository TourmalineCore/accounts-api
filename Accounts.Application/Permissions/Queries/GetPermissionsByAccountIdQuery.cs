using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Accounts.Core.Contracts;

namespace Accounts.Application.Permissions.Queries;

public class GetPermissionsByAccountIdQueryHandler
{
    private readonly IAccountRepository _accountRepository;

    public GetPermissionsByAccountIdQueryHandler(IAccountRepository accountRepository)
    {
        _accountRepository = accountRepository;
    }

    public async Task<IEnumerable<string>> Handle(long accountId)
    {
        var user = await _accountRepository.FindByIdAsync(accountId);

        return user.AccountRoles
            .Select(x => x.Role)
            .SelectMany(x => x.Permissions);
    }
}