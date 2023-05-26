using System.Threading.Tasks;

namespace Application.Contracts;

public interface ICommandHandler<in TIn>
{
    Task HandleAsync(TIn command);
}

public interface ICommandHandler<in TIn, TOut>
{
    Task<TOut> HandleAsync(TIn command);
}