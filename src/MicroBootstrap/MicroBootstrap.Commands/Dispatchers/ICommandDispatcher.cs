using System.Threading.Tasks;
using MicroBootstrap.Messages;
using MicroBootstrap.RabbitMq;

namespace MicroBootstrap.Commands.Dispatchers
{
    public interface ICommandDispatcher
    {
        Task SendAsync<T>(T command, ICorrelationContext context = null) where T : ICommand;
    }
}