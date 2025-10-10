using System.Threading.Tasks;

namespace Application.Contracts;

public interface IQueryHandler<in TIn, TOut>
{
  Task<TOut> HandleAsync(TIn query);
}

public interface IQueryHandler<TOut>
{
  Task<TOut> HandleAsync();
}
