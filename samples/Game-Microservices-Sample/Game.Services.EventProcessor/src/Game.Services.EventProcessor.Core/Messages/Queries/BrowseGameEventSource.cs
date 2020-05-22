using MicroBootstrap.Queries;
using Game.Services.EventProcessor.Core.DTO;

namespace Game.Services.EventProcessor.Core.Messages.Queries
{
    public class BrowseGameEventSource : PagedQueryBase, IQuery<PagedResult<GameEventSourceDto>>
    {
    }
}