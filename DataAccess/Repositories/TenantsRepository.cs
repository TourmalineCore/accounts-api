using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Contracts;
using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories;

internal class TenantsRepository : ITenantsRepository
{
    private readonly AccountsDbContext _context;

    public TenantsRepository(AccountsDbContext context)
    {
        _context = context;
    }

    public async Task<long> CreateAsync(Tenant tenant)
    {
        await _context.AddAsync(tenant);
        await _context.SaveChangesAsync();

        return tenant.Id;
    }

    public Task<Tenant> GetByIdAsync(long id)
    {
        return _context
            .Queryable<Tenant>()
            .Include(x => x.Accounts)
            .SingleOrDefaultAsync(x => x.Id == id)!;
    }

    public async Task<IEnumerable<Tenant>> GetAllAsync()
    {
        var tenants = await _context
            .QueryableAsNoTracking<Tenant>()
            .Include(x => x.Accounts)
            .ToListAsync();

        return tenants;
    }

    public Task RemoveAsync(Tenant tenant)
    {
        _context.Remove(tenant);
        return _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Tenant tenant)
    {
        _context.Update(tenant);
        await _context.SaveChangesAsync();
    }
}
