using Accounts.Core.Contracts;
using Accounts.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Accounts.DataAccess.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly AccountsDbContext _usersDbContext;

        public RoleRepository(AccountsDbContext usersDbContext)
        {
            _usersDbContext = usersDbContext;
        }

        public async Task<long> CreateAsync(Role role)
        {
            await _usersDbContext.AddAsync(role);
            await _usersDbContext.SaveChangesAsync();

            return role.Id;
        }

        public async Task<IEnumerable<Role>> GetRolesAsync()
        {
            return await _usersDbContext
                .Queryable<Role>()
                .Include(x => x.AccountRoles)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task UpdateRoleAsync(Role role, List<Permission> permission)
        {
            role.UpdateRole(permission);
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

            foreach (var id in roleIds)
            {
                var role = await FindOneAsync(id);
                roles.Add(role);
            }

            return roles;
        }
    }
}
