using Accounts.Core.Contracts;
using Accounts.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Accounts.DataAccess.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly AccountsDbContext _usersDbContext;

        public AccountRepository(AccountsDbContext usersDbContext)
        {
            _usersDbContext = usersDbContext;
        }

        public Task AddRoleAsync(Account user, Role role)
        {
            var accountRole = new AccountRole
            {
                Role = role,
            };

            user.AddRole(accountRole);

            return _usersDbContext.SaveChangesAsync();
        }

        public async Task<long> CreateAsync(Account role)
        {
            await _usersDbContext.AddAsync(role);
            await _usersDbContext.SaveChangesAsync();

            return role.Id;
        }

        public Task<Account?> FindByCorporateEmailAsync(string corporateEmail)
        {
            return _usersDbContext
                    .Queryable<Account>()
                    .Include(x => x.AccountRoles)
                    .ThenInclude(x => x.Role)
                    .SingleOrDefaultAsync(x => x.CorporateEmail == corporateEmail && x.DeletedAtUtc == null);
        }

        public Task<Account> FindByIdAsync(long id)
        {
            return _usersDbContext
                    .Queryable<Account>()
                    .Include(x => x.AccountRoles)
                    .ThenInclude(x => x.Role)
                    .ThenInclude(x => x.Permissions)
                    .SingleAsync(x => x.Id == id && x.DeletedAtUtc == null);
        }

        public async Task<IEnumerable<Account>> GetAllAsync()
        {
            return await _usersDbContext
                .QueryableAsNoTracking<Account>()
                .Where(x => x.DeletedAtUtc == null)
                .Include(x => x.AccountRoles)
                .ThenInclude(x => x.Role)
                .ToListAsync();
        }

        public Task RemoveAsync(Account user)
        {
            _usersDbContext.Remove(user);

            return _usersDbContext.SaveChangesAsync();
        }

        public Task UpdateAsync(Account user)
        {
            _usersDbContext.Update(user);

            return _usersDbContext.SaveChangesAsync();
        }
    }
}
