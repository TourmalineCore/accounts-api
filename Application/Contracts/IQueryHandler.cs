using System.Threading.Tasks;

namespace Application.Contracts
{
    public interface IQueryHandler<in Tin, Tout>
    {
        Task<Tout> Handle(Tin query);
    }
}
