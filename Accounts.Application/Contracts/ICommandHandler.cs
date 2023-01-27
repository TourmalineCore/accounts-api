using System.Threading.Tasks;

namespace Accounts.Application.Contracts
{
    public interface ICommandHandler<in Tin>
    {
        Task Handle(Tin command);
    }

    public interface ICommandHandler<in Tin, Tout>
    {
        Task<Tout> Handle(Tin command);
    }
}
