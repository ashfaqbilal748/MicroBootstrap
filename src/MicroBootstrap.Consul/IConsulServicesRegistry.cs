using System.Threading.Tasks;
using Consul;

namespace MicroBootstrap.Consul
{
    public interface IConsulServicesRegistry
    {
        Task<AgentService> GetAsync(string name);
    }
}