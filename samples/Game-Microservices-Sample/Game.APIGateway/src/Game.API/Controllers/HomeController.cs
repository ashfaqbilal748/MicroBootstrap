using MicroBootstrap.RabbitMq;
using Microsoft.AspNetCore.Mvc;
using OpenTracing;

namespace Game.API.Controllers
{
    [Route("")]
    public class HomeController : ControllerBase
    {   
        [HttpGet("ping")]
        public IActionResult Ping() => Ok();
    }
}