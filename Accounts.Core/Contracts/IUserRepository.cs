using Accounts.Core.Entities;
using System.Threading.Tasks;

namespace Accounts.Core.Contracts
{
    public interface IUserRepository : IRepository<Account>
    {
        public Task AddRoleAsync(Account user, Role role);

        public Task<Account?> FindByEmailAsync(string email);
    }
}
