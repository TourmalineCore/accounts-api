using System.Threading.Tasks;
using Core.Entities;

namespace Core.Contracts
{
    public interface IAccountRepository : IRepository<Account>
    {
        public Task<Account?> FindByCorporateEmailAsync(string corporateEmail);
    }
}
