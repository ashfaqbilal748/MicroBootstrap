using System.Threading.Tasks;
using MicroBootstrap.RabbitMq;
using Game.Services.Messaging.Core.Messages.Events;
using Game.Services.Messaging.Core.Services;
using MicroBootstrap.Events;

namespace Game.Services.Messaging.Application.Handlers.Events
{
    public class GameEventSourceAddedHandler : IEventHandler<GameEventSourceAdded>
    {
        private readonly IHubService _hubService;

        public GameEventSourceAddedHandler(IHubService hubService)
        {
            _hubService = hubService;
        }

        public Task HandleAsync(GameEventSourceAdded @event, ICorrelationContext context)
        {
            return Task.CompletedTask;
        }
    }
}