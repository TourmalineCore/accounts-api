using System.Threading.Tasks;

namespace Application.Contracts
{
    public interface ICommandHandler<in Tin>
    {
        Task Handle(Tin command);
    }

    public interface ICommandHandler<in Tin, Tout>
    {
        Task<Tout> HandleAsync(Tin command);
    }
}
