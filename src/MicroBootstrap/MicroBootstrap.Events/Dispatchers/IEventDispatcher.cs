using System.Threading.Tasks;
using MicroBootstrap.Messages;
using MicroBootstrap.RabbitMq;

namespace MicroBootstrap.Events.Dispatchers
{
    public interface IEventDispatcher
    {
        Task PublishAsync<T>(T @event, ICorrelationContext context = null) where T : class, IEvent;
    }
}