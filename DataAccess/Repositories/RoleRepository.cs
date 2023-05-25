using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Contracts;
using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly AccountsDbContext _usersDbContext;

        public RoleRepository(AccountsDbContext usersDbContext)
        {
            _usersDbContext = usersDbContext;
        }

        public async Task<long> CreateAsync(Role account)
        {
            await _usersDbContext.AddAsync(account);
            await _usersDbContext.SaveChangesAsync();

            return account.Id;
        }

        public async Task<IEnumerable<Role>> GetRolesAsync()
        {
            return await _usersDbContext
                .Queryable<Role>()
                .Include(x => x.AccountRoles)
                .AsNoTracking()
                .ToListAsync();
        }

        public Task<Role> GetByIdAsync(long id)
        {
            return _usersDbContext
                .Queryable<Role>()
                .GetByIdAsync(id);
        }

        public Task<Role?> FindByIdAsync(long id)
        {
            return _usersDbContext
                    .Queryable<Role>()
                    .FindByIdAsync(id);
        }

        public Task<Role> FindOneAsync(long id)
        {
            return _usersDbContext
                    .Queryable<Role>()
                    .GetByIdAsync(id);
        }

        public async Task<IEnumerable<Role>> GetAllAsync()
        {
            return await _usersDbContext
                .Roles
                .ToListAsync();
        }

        public Task RemoveAsync(Role role)
        {
            _usersDbContext.Remove(role);

            return _usersDbContext.SaveChangesAsync();
        }

        public Task UpdateAsync(Role account)
        {
            _usersDbContext.Update(account);

            return _usersDbContext.SaveChangesAsync();
        }
    }
}
