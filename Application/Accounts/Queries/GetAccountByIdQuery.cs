using System.Threading.Tasks;
using Application.Contracts;
using Core.Contracts;

namespace Application.Accounts.Queries;

public readonly struct GetAccountByIdQuery
{
    public long Id { get; init; }
}

public class GetAccountByIdQueryHandler : IQueryHandler<GetAccountByIdQuery, AccountDto>
{
    private readonly IAccountsRepository _accountsRepository;

    public GetAccountByIdQueryHandler(IAccountsRepository accountsRepository)
    {
        _accountsRepository = accountsRepository;
    }

    public async Task<AccountDto> HandleAsync(GetAccountByIdQuery request)
    {
        var account = await _accountsRepository.GetByIdAsync(request.Id);
        return new AccountDto(account);
    }
}