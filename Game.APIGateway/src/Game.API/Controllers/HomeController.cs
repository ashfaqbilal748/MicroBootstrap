using Common.RabbitMq;
using Microsoft.AspNetCore.Mvc;
using OpenTracing;

namespace Game.API.Controllers
{
    [Route("")]
    public class HomeController : BaseController
    {
        public HomeController(IBusPublisher busPublisher, ITracer tracer) : base(busPublisher, tracer)
        {
        }
        
        [HttpGet("ping")]
        public IActionResult Ping() => Ok();
    }
}