
using System.Threading.Tasks;
using Common.Dispatchers;
using Microsoft.AspNetCore.Mvc;

namespace Game.Services.EventProcessor.API.Controllers
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
