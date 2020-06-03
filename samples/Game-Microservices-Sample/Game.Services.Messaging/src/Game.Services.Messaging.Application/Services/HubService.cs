using System.Threading.Tasks;
using Game.Services.Messaging.Core.Messages.Events;
using Game.Services.Messaging.Core.Services;

namespace Game.Services.Messaging.Application.Services
{
    public class HubService : IHubService
    {
        private readonly IHubWrapper _hubContextWrapper;

        public HubService(IHubWrapper hubContextWrapper)
        {
            _hubContextWrapper = hubContextWrapper;
        }

        public async Task PublishGameEventSourceAddedAsync(GameEventSourceAdded @event)
        {
            await _hubContextWrapper.PublishToAllAsync("game_event_source_added",
                new
                {
                    id = @event.Id,
                    score = @event.Score,
                    isWin = @event.IsWin
                }
            );
        }
    }
}