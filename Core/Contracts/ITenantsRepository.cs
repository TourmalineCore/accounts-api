using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Entities;

namespace Core.Contracts;

public interface ITenantsRepository
{
    Task<long> CreateAsync(Tenant tenant);
    Task<Tenant> GetByIdAsync(long id);
    Task<IEnumerable<Tenant>> GetAllAsync();
    Task RemoveAsync(Tenant tenant);
    Task UpdateAsync(Tenant tenant);
}
