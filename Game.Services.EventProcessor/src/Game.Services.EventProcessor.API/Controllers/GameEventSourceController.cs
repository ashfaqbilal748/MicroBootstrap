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
    [Route("game-event-sources")]
    public class GameEventSourcesController : BaseController
    {
        public GameEventSourcesController(IDispatcher dispatcher) : base(dispatcher)
        {
        }

        [HttpGet]
        public async Task<ActionResult<PagedResult<GameEventSourceDto>>> Get([FromQuery] BrowseGameEventSource query)
            => Collection(await QueryAsync(query));

        [HttpGet("{id}")]
        public async Task<ActionResult<GameEventSourceDto>> Get([FromRoute] GetGameEventSource query)
            => Single(await QueryAsync(query));
        
        [HttpPost]
        public async Task<ActionResult> Post(AddGameEventSource command)
        {
            await SendAsync(command.BindId(c => c.Id));
            return Accepted();
        }
    }
}