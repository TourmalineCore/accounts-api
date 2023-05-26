using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Contracts;
using Core.Entities;
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
            .Include(x => x.AccountRoles)
            .ThenInclude(x => x.Role)
            .GetByIdAsync(id);
    }

    public async Task<IEnumerable<Account>> GetAllAsync()
    {
        return await _context
            .QueryableAsNoTracking<Account>()
            .Where(x => x.DeletedAtUtc == null)
            .Include(x => x.AccountRoles)
            .ThenInclude(x => x.Role)
            .ToListAsync();
    }

    public Task RemoveAsync(Account account)
    {
        _context.Remove(account);
        return _context.SaveChangesAsync();
    }

    public Task UpdateAsync(Account account)
    {
        _context.Update(account);
        return _context.SaveChangesAsync();
    }
}