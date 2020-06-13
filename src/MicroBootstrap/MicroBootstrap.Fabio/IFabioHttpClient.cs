using System.Threading.Tasks;

namespace MicroBootstrap.Fabio
{
    public interface IFabioHttpClient
    {
        Task<T> GetAsync<T>(string requestUri);
    }
}