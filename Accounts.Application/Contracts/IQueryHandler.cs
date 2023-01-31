using System.Threading.Tasks;

namespace Accounts.Application.Contracts
{
    public interface IQueryHandler<in Tin, Tout>
    {
        Task<Tout> Handle(Tin query);
    }
}
