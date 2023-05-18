using Accounts.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Accounts.Core.Contracts
{
    public interface IRoleRepository : IRepository<Role>
    {
        public Task<IEnumerable<Role>> GetRolesAsync();

        public Task UpdateRoleAsync(Role role, List<Permission> permission);

        public Task<List<Role>> FindListAsync(List<long> ids);
    }
}
