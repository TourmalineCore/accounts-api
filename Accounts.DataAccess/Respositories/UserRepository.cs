using Accounts.Core.Contracts;
using Accounts.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Accounts.DataAccess.Respositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AccountsDbContext _usersDbContext;

        public UserRepository(AccountsDbContext usersDbContext)
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

        public async Task<long> CreateAsync(Account user)
        {
            await _usersDbContext.AddAsync(user);
            await _usersDbContext.SaveChangesAsync();

            return user.Id;
        }

        public Task<Account?> FindByEmailAsync(string email)
        {
            return _usersDbContext
                    .Queryable<Account>()
                    .Include(x => x.AccountRoles)
                    .ThenInclude(x => x.Role)
                    .ThenInclude(x => x.Privileges)
                    .SingleOrDefaultAsync(x => x.Email == email && x.DeletedAtUtc == null);
        }

        public Task<Account> FindByIdAsync(long id)
        {
            return _usersDbContext
                    .Queryable<Account>()
                    .Include(x => x.AccountRoles)
                    .ThenInclude(x => x.Role)
                    .ThenInclude(x => x.Privileges)
                    .SingleAsync(x => x.Id == id && x.DeletedAtUtc == null);
        }

        public async Task<IEnumerable<Account>> GetAllAsync()
        {
            return await _usersDbContext
                .QueryableAsNoTracking<Account>()
                .Where(x => x.DeletedAtUtc == null)
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
