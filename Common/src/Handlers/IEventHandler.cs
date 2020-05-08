using Common.RabbitMq;
using Common.Messages;
using System.Threading.Tasks;

namespace Common.Handlers
{
    public interface IEventHandler<in TEvent> where TEvent : IEvent
    {
        Task HandleAsync(TEvent @event, ICorrelationContext context);
    }
}