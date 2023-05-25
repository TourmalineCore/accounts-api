using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Entities;

namespace Core.Contracts
{
    public interface IRepository<TEntity> where TEntity : IIdentityEntity
    {
        public Task<long> CreateAsync(TEntity role);

        public Task<TEntity?> FindByIdAsync(long id);

        public Task<TEntity> GetByIdAsync(long id);

        public Task<IEnumerable<TEntity>> GetAllAsync();

        public Task RemoveAsync(TEntity entity);

        public Task UpdateAsync(TEntity account);
    }
}
