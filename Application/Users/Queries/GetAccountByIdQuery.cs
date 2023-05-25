using System.Threading.Tasks;
using Application.Contracts;
using Core.Contracts;

namespace Application.Users.Queries
{
    public class GetAccountByIdQuery
    {
        public long Id { get; init; }
    }
    public class GetAccountByIdQueryHandler : IQueryHandler<GetAccountByIdQuery, AccountDto>
    {
        private readonly IAccountRepository _accountRepository;

        public GetAccountByIdQueryHandler(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public async Task<AccountDto> Handle(GetAccountByIdQuery request)
        {
            var account = await _accountRepository.FindByIdAsync(request.Id);
            return new AccountDto(account);
        }
    }
}
