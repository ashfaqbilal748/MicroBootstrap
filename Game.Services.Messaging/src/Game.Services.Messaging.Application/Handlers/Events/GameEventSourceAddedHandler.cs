using System.Threading.Tasks;
using Common.Handlers;
using Common.RabbitMq;
using Game.Services.Messaging.Core.Messages.Events;
using Game.Services.Messaging.Core.Services;

namespace Game.Services.Messaging.Application.Handlers.Events
{
    public class GameEventSourceAddedHandler: IEventHandler<GameEventSourceAdded>
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