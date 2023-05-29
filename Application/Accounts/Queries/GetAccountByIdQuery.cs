using System.Threading.Tasks;
using Application.Contracts;
using Core.Contracts;

namespace Application.Accounts.Queries;

public readonly struct GetAccountByIdQuery
{
    public long Id { get; init; }

    public string CallerCorporateEmail { get; init; }
}

public class GetAccountByIdQueryHandler : IQueryHandler<GetAccountByIdQuery, AccountDto>
{
    private readonly IAccountsRepository _accountsRepository;

    public GetAccountByIdQueryHandler(IAccountsRepository accountsRepository)
    {
        _accountsRepository = accountsRepository;
    }

    public async Task<AccountDto> HandleAsync(GetAccountByIdQuery query)
    {
        var account = await _accountsRepository.GetByIdAsync(query.Id);
        return new AccountDto(account, query.CallerCorporateEmail);
    }
}