using System;
using System.Linq;
using System.Threading.Tasks;
using Core.Contracts;
using Microsoft.EntityFrameworkCore;

namespace DataAccess;

public static class QueryableExtensions
{
    public static IQueryable<TEntity> QueryableAsNoTracking<TEntity>(this DbContext context)
        where TEntity : class
    {
        return context.Queryable<TEntity>().AsNoTracking();
    }

    public static IQueryable<TEntity> Queryable<TEntity>(this DbContext context)
        where TEntity : class
    {
        return context.Set<TEntity>();
    }

    public static async Task<TEntity> GetByIdAsync<TEntity>(this IQueryable<TEntity> queryable, long id)
        where TEntity : class, IEntity
    {
        var entity = await FindByIdAsync(queryable, id);
        return entity ?? throw new NullReferenceException("Entity does not exits");
    }

    public static async Task<TEntity?> FindByIdAsync<TEntity>(this IQueryable<TEntity> queryable, long id)
        where TEntity : class, IEntity
    {
        return await queryable.SingleOrDefaultAsync(x => x.Id == id);
    }
}