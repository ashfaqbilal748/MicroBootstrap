using System.Threading.Tasks;

namespace MicroBootstrap.Consul
{
    public interface IConsulHttpClient
    {
        Task<T> GetAsync<T>(string requestUri);
    }
}

