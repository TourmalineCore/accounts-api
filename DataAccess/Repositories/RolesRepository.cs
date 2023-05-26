using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Contracts;
using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories;

public class RolesRepository : IRolesRepository
{
    private readonly AccountsDbContext _context;

    public RolesRepository(AccountsDbContext context)
    {
        _context = context;
    }

    public async Task<long> CreateAsync(Role role)
    {
        await _context.AddAsync(role);
        await _context.SaveChangesAsync();

        return role.Id;
    }

    public async Task<IEnumerable<Role>> GetRolesAsync()
    {
        return await _context
            .Queryable<Role>()
            .Include(x => x.AccountRoles)
            .AsNoTracking()
            .ToListAsync();
    }

    public Task<Role> GetByIdAsync(long id)
    {
        return _context
            .Queryable<Role>()
            .GetByIdAsync(id);
    }

    public Task<Role?> FindByIdAsync(long id)
    {
        return _context
            .Queryable<Role>()
            .FindByIdAsync(id);
    }

    public async Task<IEnumerable<Role>> GetAllAsync()
    {
        return await _context
            .Roles
            .ToListAsync();
    }

    public Task RemoveAsync(Role role)
    {
        _context.Remove(role);
        return _context.SaveChangesAsync();
    }

    public Task UpdateAsync(Role role)
    {
        _context.Update(role);
        return _context.SaveChangesAsync();
    }
}