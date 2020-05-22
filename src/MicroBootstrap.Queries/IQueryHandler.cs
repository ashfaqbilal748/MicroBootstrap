using System.Threading.Tasks;
using MicroBootstrap.Types;

namespace MicroBootstrap.Queries
{
    public interface IQueryHandler<TQuery,TResult> where TQuery : IQuery<TResult>
    {
        Task<TResult> HandleAsync(TQuery query);
    }
}