
using Common.Dispatchers;
using Microsoft.AspNetCore.Mvc;

namespace Game.Services.Messaging.API.Controller
{
    [Route("")]
    public class HomeController : BaseController
    {
        public HomeController(IDispatcher dispatcher) : base(dispatcher)
        {
        }

        [HttpGet("ping")]
        public IActionResult Ping() => Ok();
    }
}