using Accounts.Core.Contracts;
using Accounts.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Accounts.DataAccess.Repositories
{
    public class PermissionsRepository : IPermissionsRepository
    {
        private readonly AccountsDbContext _usersDbContext;

        public PermissionsRepository(AccountsDbContext usersDbContext)
        {
            _usersDbContext = usersDbContext;
        }

        public async Task<long> CreateAsync(Permission account)
        {
            await _usersDbContext.AddAsync(account);
            await _usersDbContext.SaveChangesAsync();

            return account.Id;
        }

        public Task<Permission> FindByIdAsync(long id)
        {
            return _usersDbContext
                   .Queryable<Permission>()
                   .GetByIdAsync(id);
        }

        public async Task<IEnumerable<Permission>> GetAllAsync()
        {
            return await _usersDbContext
                .QueryableAsNoTracking<Permission>()
                .ToListAsync();
        }

        public Task RemoveAsync(Permission permission)
        {
            _usersDbContext.Remove(permission);

            return _usersDbContext.SaveChangesAsync();
        }

        public Task UpdateAsync(Permission permission)
        {
            _usersDbContext.Update(permission);

            return _usersDbContext.SaveChangesAsync();
        }
    }
}
