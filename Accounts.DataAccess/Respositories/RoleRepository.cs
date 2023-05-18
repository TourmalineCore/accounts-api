using Accounts.Core.Contracts;
using Accounts.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Accounts.DataAccess.Respositories
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
                .Include(x => x.Privileges)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task UpdateRoleAsync(Role role, List<Privilege> privileges)
        {
            role.UpdateRole(privileges);
            await _usersDbContext.SaveChangesAsync();
        }

        public Task<Role> FindByIdAsync(long id)
        {
            return _usersDbContext
                    .Queryable<Role>()
                    .GetByIdAsync(id);
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
                .QueryableAsNoTracking<Role>()
                .Include(x => x.Privileges)
                .ToListAsync();
        }

        public Task RemoveAsync(Role role)
        {
            _usersDbContext.Remove(role);

            return _usersDbContext.SaveChangesAsync();
        }

        public Task UpdateAsync(Role role)
        {
            _usersDbContext.Update(role);

            return _usersDbContext.SaveChangesAsync();
        }

        public async Task<List<Role>> FindListAsync(List<long> roleIds)
        {
            var roles = new List<Role>();
                  
            foreach(var id in roleIds)
            {
                var role = await FindOneAsync(id);
                roles.Add(role);
            }

            return roles;
        }
    }
}
