using Accounts.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Accounts.Core.Contracts
{
    public interface IRepository<TEntity> where TEntity : IIdentityEntity
    {
        public Task<long> CreateAsync(TEntity account);

        public Task<TEntity> FindByIdAsync(long id);

        public Task<IEnumerable<TEntity>> GetAllAsync();

        public Task RemoveAsync(TEntity entity);

        public Task UpdateAsync(TEntity entity);
    }
}
