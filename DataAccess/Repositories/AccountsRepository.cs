using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Contracts;
using Core.Entities;
using Core.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories;

public class AccountsRepository : IAccountsRepository
{
    private readonly AccountsDbContext _context;

    public AccountsRepository(AccountsDbContext context)
    {
        _context = context;
    }

    public async Task<long> CreateAsync(Account account)
    {
        if (account.IsAdmin)
        {
            throw new AccountOperationException("Can't create one more admin");
        }

        await _context.AddAsync(account);
        await _context.SaveChangesAsync();

        return account.Id;
    }

    public Task<Account?> FindByCorporateEmailAsync(string corporateEmail)
    {
        return _context
            .Queryable<Account>()
            .Include(x => x.AccountRoles)
            .ThenInclude(x => x.Role)
            .SingleOrDefaultAsync(x => x.CorporateEmail == corporateEmail && x.DeletedAtUtc == null);
    }

    public Task<Account?> FindByIdAsync(long id)
    {
        return _context
            .Queryable<Account>()
            .Include(x => x.AccountRoles)
            .ThenInclude(x => x.Role)
            .FindByIdAsync(id);
    }

    public Task<Account> GetByIdAsync(long id)
    {
        return _context
            .Queryable<Account>()
            .Include(x => x.Tenant)
            .Include(x => x.AccountRoles)
            .ThenInclude(x => x.Role)
            .GetByIdAsync(id);
    }

    public async Task<IEnumerable<Account>> GetAllAsync()
    {
        var accounts = await _context
            .QueryableAsNoTracking<Account>()
            .Include(x => x.AccountRoles)
            .ThenInclude(x => x.Role)
            .Include(x => x.Tenant)
            .Where(x => x.DeletedAtUtc == null)
            .ToListAsync();

        return accounts.Where(account => !account.IsAdmin);
    }

    public Task RemoveAsync(Account account)
    {
        if (account.IsAdmin)
        {
            throw new AccountOperationException("Can't remove admin");
        }

        _context.Remove(account);
        return _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Account account)
    {
        if (account.IsAdmin)
        {
            throw new AccountOperationException("Can't edit admin");
        }

        _context.Update(account);
        await _context.SaveChangesAsync();
    }
}