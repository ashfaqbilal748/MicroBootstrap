using System.Threading.Tasks;
using Common.Dispatchers;
using Common.Types;
using Common.WebApi;
using Game.Services.EventProcessor.Core.DTO;
using Game.Services.EventProcessor.Core.Messages.Commands;
using Game.Services.EventProcessor.Core.Messages.Queries;
using Microsoft.AspNetCore.Mvc;

namespace Game.Services.EventProcessor.API.Controllers
{
    public class GameEventSourceController : BaseController
    {
        public GameEventSourceController(IDispatcher dispatcher) : base(dispatcher)
        {
        }

        [HttpGet("game-event-sources")]
        public async Task<ActionResult<PagedResult<GameEventSourceDto>>> Get([FromQuery] BrowseGameEventSource query)
            => Collection(await QueryAsync(query));

        [HttpGet("game-event-sources/{id}")]
        public async Task<ActionResult<GameEventSourceDto>> Get([FromRoute] GetGameEventSource query)
            => Single(await QueryAsync(query));
        
        [HttpPost("game-event-sources")]
        public async Task<ActionResult> Post(AddGameEventSource command)
        {
            await SendAsync(command.BindId(c => c.Id));
            return Accepted();
        }
    }
}