using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Contracts;

public interface IRepository<TEntity> where TEntity : IEntity
{
    public Task<IEnumerable<TEntity>> GetAllAsync();

    public Task<TEntity> GetByIdAsync(long id);

    public Task<TEntity?> FindByIdAsync(long id);

    public Task<long> CreateAsync(TEntity role);

    public Task DeleteAsync(TEntity entity);

    public Task UpdateAsync(TEntity account);
}