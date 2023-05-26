using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Entities;

namespace Core.Contracts;

public interface IRolesRepository : IRepository<Role>
{
    public Task<IEnumerable<Role>> GetRolesAsync();
}