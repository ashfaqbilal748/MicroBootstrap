
using MicroBootstrap.Commands.Dispatchers;
using MicroBootstrap.Queries.Dispatchers;
using Microsoft.AspNetCore.Mvc;

namespace Game.Services.Messaging.API.Controller
{
    [Route("")]
    public class HomeController : BaseController
    {
        public HomeController(ICommandDispatcher commandDispatcher, IQueryDispatcher queryDispatcher) : base(commandDispatcher, queryDispatcher)
        {
        }
        
        [HttpGet("ping")]
        public IActionResult Ping() => Ok();
    }
}