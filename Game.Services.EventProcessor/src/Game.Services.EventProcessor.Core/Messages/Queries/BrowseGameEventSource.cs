using Common.Types;
using Game.Services.EventProcessor.Core.DTO;

namespace Game.Services.EventProcessor.Core.Messages.Queries
{
    public class BrowseGameEventSource : PagedQueryBase, Common.Types.IQuery<PagedResult<GameEventSourceDto>>
    {
    }
}