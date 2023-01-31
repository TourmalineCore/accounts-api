using Accounts.Core.Contracts;
using Accounts.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Accounts.DataAccess.Respositories
{
    public class PrivilegeRepository : IPrivilegeRepository
    {
        private readonly AccountsDbContext _usersDbContext;

        public PrivilegeRepository(AccountsDbContext usersDbContext)
        {
            _usersDbContext = usersDbContext;
        }

        public async Task<long> CreateAsync(Privilege account)
        {
            await _usersDbContext.AddAsync(account);
            await _usersDbContext.SaveChangesAsync();

            return account.Id;
        }

        public Task<Privilege> FindByIdAsync(long id)
        {
            return _usersDbContext
                   .Queryable<Privilege>()
                   .GetByIdAsync(id);
        }

        public async Task<IEnumerable<Privilege>> GetAllAsync()
        {
            return await _usersDbContext
                .QueryableAsNoTracking<Privilege>()
                .ToListAsync();
        }

        public Task RemoveAsync(Privilege privilege)
        {
            _usersDbContext.Remove(privilege);

            return _usersDbContext.SaveChangesAsync();
        }

        public Task UpdateAsync(Privilege privilege)
        {
            _usersDbContext.Update(privilege);

            return _usersDbContext.SaveChangesAsync();
        }
    }
}
