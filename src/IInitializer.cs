using System.Threading.Tasks;

namespace MicroBootstrap
{
    public interface IInitializer
    {
        Task InitializeAsync();
    }
}