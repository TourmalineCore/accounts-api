using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Contracts;
using Core.Contracts;

namespace Application.Users.Queries
{
    public partial class GetAccountsQuery
    {
    }

    public class GetAccountsQueryHandler : IQueryHandler<GetAccountsQuery, IEnumerable<AccountDto>>
    {
        private readonly IAccountRepository _accountRepository;

        public GetAccountsQueryHandler(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public async Task<IEnumerable<AccountDto>> Handle(GetAccountsQuery request)
        {
            var accounts = await _accountRepository.GetAllAsync();
            return accounts.Select(account => new AccountDto(account));
        }
    }
}
